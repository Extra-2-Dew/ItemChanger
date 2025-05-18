using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ID2.ItemChanger;

public class NotificationHandler : MonoBehaviour
{
	private static NotificationHandler instance;
	private const float defaultDisplayTime = 3f;
	private const float reduceTimeForMessagesThreshold = 6;
	private readonly List<MessageBox> messageQueue = new();
	private MessageBox currentMessage;
	private EntityHUDData hudData;

	public static NotificationHandler Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject obj = new("NotificationHandler");
				instance = obj.AddComponent<NotificationHandler>();
				DontDestroyOnLoad(obj);
			}

			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
		hudData = Resources.Load<EntityHUDData>("HUDs/XboneHUDData");
	}

	private void Update()
	{
		bool canShowMessage = currentMessage == null || !currentMessage.IsActive;

		if (canShowMessage && messageQueue.Count > 0)
		{
			// Show oldest message box in the queue (is always at index 0)
			currentMessage = messageQueue[0];
			currentMessage.messageData.DisplayTime = messageQueue.Count < reduceTimeForMessagesThreshold
				? currentMessage.messageData.DisplayTime
				: 1;
			currentMessage.Show();
			messageQueue.RemoveAt(0);
		}
	}

	public static void ShowNotification(string message, string icon, float displayTime = defaultDisplayTime)
	{
		MessageBox.Data data = new()
		{
			Message = message,
			IconName = icon,
			DisplayTime = displayTime
		};
		Instance.ShowNotification(data);
	}

	public static void ShowNotification(string message, Texture2D icon, float displayTime = defaultDisplayTime)
	{
		MessageBox.Data data = new()
		{
			Message = message,
			Icon = icon,
			DisplayTime = displayTime
		};
		Instance.ShowNotification(data);
	}

	public static void HideAllNotifications(bool clearQueue = true)
	{
		Instance.currentMessage?.Hide();

		if (clearQueue)
		{
			Instance.messageQueue.Clear();
		}
	}

	private void ShowNotification(MessageBox.Data data)
	{
		MessageBox messageBox = new(data);
		Instance.messageQueue.Add(messageBox);
	}

	public class MessageBox(MessageBox.Data data)
	{
		private ItemMessageBox messageBox;

		public bool IsActive
		{
			get
			{
				return messageBox != null && messageBox.IsActive;
			}
		}

		public Data messageData = data;
		private List<ColorSegment> colorSegments = new();

		public void Show()
		{
			if (messageBox == null)
			{
				messageBox = OverlayWindow.GetPooledWindow(Instance.hudData.GetItemBox);
				GetColorSegments();
				SetIconTexture();
				messageData.Message = Regex.Replace(messageData.Message, @"<color=.*?>|</color>", "");
				messageBox._text.StringText = new StringHolder.OutString(messageData.Message);
				Instance.StartCoroutine(SetTextColors());
				ResizeBox();
				messageBox.timer = messageData.DisplayTime;
				messageBox.countdown = messageData.DisplayTime > 0;
			}

			if (messageBox._tweener != null)
				messageBox._tweener.Show(true);
			else
				messageBox.gameObject.SetActive(true);
		}

		public void Hide()
		{
			if (messageBox == null)
				return;

			if (messageBox._tweener != null)
				messageBox._tweener.Hide(true);
			else
				messageBox.gameObject.SetActive(false);
		}

		private void SetIconTexture()
		{
			Texture2D texture = messageData.Icon;

			// If texture is a custom predefined one, load it
			if (messageData.IconName.StartsWith("Custom"))
			{
				string iconName = messageData.IconName.Substring(messageData.IconName.LastIndexOf('/'));
				string iconPath = $"{PluginInfo.PLUGIN_NAME}/Resources/Item Icons/{iconName}.png";
				texture = ModCore.Utility.GetTextureFromFile(iconPath);
			}
			// If texture is not given from another mod, load from Resources
			else if (texture == null)
			{
				string iconPath = $"Items/ItemIcon_{messageData.IconName}";
				texture = Resources.Load(iconPath) as Texture2D;
			}

			// If texture is still null at this point, it's not vanilla or custom
			if (texture == null)
			{
				Logger.LogWarning($"Could not load texture from Resources: '{messageData.IconName}' for the message: '{messageData.Message}'");
				string iconPath = $"{PluginInfo.PLUGIN_NAME}/Resources/Item Icons/Unknown.png";
				texture = ModCore.Utility.GetTextureFromFile(iconPath);
			}

			messageBox.texture = texture;
			messageBox.mat.mainTexture = texture;
		}

		private void GetColorSegments()
		{
			string pattern = @"<color=(.*?)>(.*?)<\/color>";
			MatchCollection matches = Regex.Matches(messageData.Message, pattern);

			if (matches.Count == 0)
			{
				return;
			}

			foreach (Match match in matches)
			{
				string colorStr = match.Groups[1].Value;
				string text = match.Groups[2].Value;

				if (ColorUtility.TryParseHtmlString(colorStr, out Color color))
				{
					colorSegments.Add(new ColorSegment() { Text = text, Color = color });
				}
			}
		}

		private IEnumerator SetTextColors()
		{
			// Arbitrary delay due to needing the TextMesh to exist before we do this
			yield return new WaitForEndOfFrame();

			if (colorSegments.Count == 0)
			{
				yield return null;
			}

			Color[] meshColors = messageBox._text.mesh.colors;
			ReplaceMeshColors(meshColors, colorSegments);
			messageBox._text.mesh.colors = meshColors;
		}

		private void ReplaceMeshColors(Color[] originalColors, List<ColorSegment> segments)
		{
			// Remove spaces from message so color indices don't get thrown off by them, as
			// Each space has one index for color
			string messageWithoutSpaces = messageData.Message.Replace(" ", "");

			foreach (ColorSegment segment in segments)
			{
				string text = segment.Text.Replace(" ", "");
				int startIndex = messageWithoutSpaces.IndexOf(text) * 4;
				int endIndex = Mathf.Min(startIndex + (text.Length * 4), originalColors.Length);

				for (int i = startIndex; i < endIndex; i++)
				{
					originalColors[i] = segment.Color;
				}
			}
		}

		/// <summary>
		/// Enlarges the box to fit text, but doesn't shrink it below 3 lines
		/// </summary>
		private void ResizeBox()
		{
			Vector2 scaledTextSize = messageBox._text.ScaledTextSize;
			Vector3 vector = messageBox._text.transform.localPosition - messageBox.backOrigin;
			scaledTextSize.y += Mathf.Abs(vector.y) + messageBox._border;
			scaledTextSize.y = Mathf.Max(messageBox.minSize.y, scaledTextSize.y);
			scaledTextSize.x = messageBox._background.ScaledSize.x;
			messageBox._background.ScaledSize = scaledTextSize;
		}

		public struct Data
		{
			public string Message { get; set; }
			public string IconName { get; set; }
			public Texture2D Icon { get; set; }
			public float DisplayTime { get; set; } = defaultDisplayTime;

			public Data() { }
		}

		private struct ColorSegment
		{
			public string Text { get; set; }
			public Color Color { get; set; }
		}
	}
}