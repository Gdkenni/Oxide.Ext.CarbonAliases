﻿using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using Oxide.Core;
using Oxide.Game.Rust.Cui;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.CarbonAliases;

public class LUI : IDisposable
{
	public readonly List<LuiContainer> elements = new();

	private readonly CUI _parent;

	/// <summary>
	/// Boolean that changes default generation of element names.
	/// With this option disabled, you cannot create UI hierarchy without manual name input.
	/// </summary>
	public bool generateNames = true;

	public LUI(CUI cui)
	{
		_parent = cui;
	}

	/// <summary>
	/// Name of last created container. Used for easy new element creation to last parent without creating variable for that.
	/// </summary>
	public string lastName = string.Empty;

	#region Core Panel

	public LuiContainer CreateParent(CUI.ClientPanels parent, LuiPosition position, string name = "") => CreateParent(_parent.GetClientPanel(parent), position, name);

	public LuiContainer CreateParent(LuiContainer container, LuiPosition position, string name = "") => CreateParent(container.name, position, name);
	public LuiContainer CreateParent(string parent, LuiPosition position, string name = "")
	{
		LuiContainer cont = new();
		cont.parent = parent;
		if (name != string.Empty)
			cont.name = name;
		else if (generateNames)
			cont.name = _parent.AppendId();
		cont.SetAnchors(position);
		elements.Add(cont);
		return cont;
	}

	#endregion

	#region Updates

	public LuiContainer UpdatePosition(string name, LuiPosition pos)
	{
		LuiContainer cont = new();
		cont.name = name;
		cont.update = true;
		cont.SetAnchors(pos);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer UpdatePosition(string name, LuiOffset off)
	{
		LuiContainer cont = new();
		cont.name = name;
		cont.update = true;
		cont.SetOffset(off);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer UpdatePosition(string name, LuiPosition pos, LuiOffset off)
	{
		LuiContainer cont = new();
		cont.name = name;
		cont.update = true;
		cont.SetAnchorAndOffset(pos, off);
		elements.Add(cont);
		return cont;
	}

	/// <summary>
	/// Creates update container without any fields assigned.
	/// </summary>
	public LuiContainer Update(string name)
	{
		LuiContainer cont = new();
		cont.name = name;
		cont.update = true;
		elements.Add(cont);
		return cont;
	}

	public LuiContainer UpdateColor(string name, string color)
	{
		LuiContainer cont = new();
		cont.name = name;
		cont.update = true;
		cont.SetColor(color);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer UpdateText(string name, string text, int fontSize = 0, string color = null)
	{
		LuiContainer cont = new();
		cont.name = name;
		cont.update = true;
		cont.SetText(text, fontSize, color, update: true);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer UpdateButtonCommand(string name, string command, bool isProtected = true)
	{
		LuiContainer cont = new();
		cont.name = name;
		cont.update = true;
		cont.SetButton(command);
		elements.Add(cont);
		return cont;
	}

	#endregion

	#region Panel Creation

	/// <summary>
	/// Creates empty container without anything. Shouldn't be used outside LUI library, but in rare cases might be useful.
	/// </summary>
	public LuiContainer CreateEmptyContainer(string parent, string name = "")
	{
		LuiContainer cont = new();
		cont.parent = parent;
		if (name != string.Empty)
			cont.name = name;
		else if (generateNames)
		{
			string newName = _parent.Manager.AppendId();
			lastName = newName;
			cont.name = newName;
		}
		return cont;
	}

	public LuiContainer CreatePanel(LuiContainer container, LuiPosition position, LuiOffset offset, string color, string name = "") => CreatePanel(container.name, position, offset, color, name);
	public LuiContainer CreatePanel(LuiContainer container, LuiOffset offset, string color, string name = "") => CreatePanel(container.name, LuiPosition.None, offset, color, name);
	public LuiContainer CreatePanel(string parent, LuiOffset offset, string color, string name = "") => CreatePanel(parent, LuiPosition.None, offset, color, name);

	public LuiContainer CreatePanel(string parent, LuiPosition position, LuiOffset offset, string color, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetColor(color);
		elements.Add(cont);
		return cont;
	}
	public LuiContainer CreateText(LuiContainer container, LuiPosition position, LuiOffset offset, int fontSize, string color, string text, TextAnchor alignment = TextAnchor.UpperLeft, string name = "") => CreateText(container.name, position, offset, fontSize, color, text, alignment, name);
	public LuiContainer CreateText(LuiContainer container, LuiOffset offset, int fontSize, string color, string text, TextAnchor alignment = TextAnchor.UpperLeft, string name = "") => CreateText(container.name, LuiPosition.None, offset, fontSize, color, text, alignment, name);
	public LuiContainer CreateText(string parent, LuiOffset offset, int fontSize, string color, string text, TextAnchor alignment = TextAnchor.UpperLeft, string name = "") => CreateText(parent, LuiPosition.None, offset, fontSize, color, text, alignment, name);

	public LuiContainer CreateText(string parent, LuiPosition position, LuiOffset offset, int fontSize, string color, string text, TextAnchor alignment = TextAnchor.MiddleCenter, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetText(text, fontSize, color, alignment);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateSprite(LuiContainer container, LuiPosition position, LuiOffset offset, string sprite, string color = LuiColors.White, string name = "") => CreateSprite(container.name, position, offset, sprite, color, name);
	public LuiContainer CreateSprite(LuiContainer container, LuiOffset offset, string sprite, string color = LuiColors.White, string name = "") => CreateSprite(container.name, LuiPosition.None, offset, sprite, color, name);
	public LuiContainer CreateSprite(string parent, LuiOffset offset, string sprite, string color = LuiColors.White, string name = "") => CreateSprite(parent, LuiPosition.None, offset, sprite, color, name);

	public LuiContainer CreateSprite(string parent, LuiPosition position, LuiOffset offset, string sprite, string color = LuiColors.White, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetSprite(sprite, color);
		elements.Add(cont);

		return cont;
	}

	public LuiContainer CreateImage(LuiContainer container, LuiPosition position, LuiOffset offset, uint png, string color = LuiColors.White, string name = "") => CreateImage(container.name, position, offset, png, color, name);
	public LuiContainer CreateImage(LuiContainer container, LuiOffset offset, uint png, string color = LuiColors.White, string name = "") => CreateImage(container.name, LuiPosition.None, offset, png, color, name);
	public LuiContainer CreateImage(string parent, LuiOffset offset, uint png, string color = LuiColors.White, string name = "") => CreateImage(parent, LuiPosition.None, offset, png, color, name);

	public LuiContainer CreateImage(string parent, LuiPosition position, LuiOffset offset, uint png, string color = LuiColors.White, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetImage(png, color);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateImageFromDb(LuiContainer container, LuiPosition position, LuiOffset offset, string dbName, string color = LuiColors.White, string name = "") => CreateImageFromDb(container.name, position, offset, dbName, color, name);
	public LuiContainer CreateImageFromDb(LuiContainer container, LuiOffset offset, string dbName, string color = LuiColors.White, string name = "") => CreateImageFromDb(container.name, LuiPosition.None, offset, dbName, color, name);
	public LuiContainer CreateImageFromDb(string parent, LuiOffset offset, string dbName, string color = LuiColors.White, string name = "") => CreateImageFromDb(parent, LuiPosition.None, offset, dbName, color, name);

	public LuiContainer CreateImageFromDb(string parent, LuiPosition position, LuiOffset offset, string dbName, string color = LuiColors.White, string name = "")
	{
		ImageDatabaseModule imageDb = BaseModule.GetModule<ImageDatabaseModule>();
		LuiContainer cont = CreateEmptyContainer(parent, name);
		uint img = imageDb.GetImage(dbName);
		if (img != 0)
		{
			cont.SetAnchorAndOffset(position, offset);
			cont.SetImage(img, color);
		}
		else
		{
			Interface.Oxide.LogWarning($"[LUI] You're trying to image from ImageDatabase '{dbName}' which doesn't exist. Ignoring.");
		}
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateUrlImage(LuiContainer container, LuiPosition position, LuiOffset offset, string url, string color = LuiColors.White, string name = "") => CreateUrlImage(container.name, position, offset, url, color, name);
	public LuiContainer CreateUrlImage(LuiContainer container, LuiOffset offset, string url, string color = LuiColors.White, string name = "") => CreateUrlImage(container.name, LuiPosition.None, offset, url, color, name);
	public LuiContainer CreateUrlImage(string parent, LuiOffset offset, string url, string color = LuiColors.White, string name = "") => CreateUrlImage(parent, LuiPosition.None, offset, url, color, name);

	public LuiContainer CreateUrlImage(string parent, LuiPosition position, LuiOffset offset, string url, string color = LuiColors.White, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetUrlImage(url, color);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateItemIcon(LuiContainer container, LuiPosition position, LuiOffset offset, string shortname, ulong skinId = 0, string name = "") => CreateItemIcon(container.name, position, offset, shortname, skinId, name);
	public LuiContainer CreateItemIcon(LuiContainer container, LuiOffset offset, string shortname, ulong skinId = 0, string name = "") => CreateItemIcon(container.name, LuiPosition.None, offset, shortname, skinId, name);
	public LuiContainer CreateItemIcon(string parent, LuiOffset offset, string shortname, ulong skinId = 0, string name = "") => CreateItemIcon(parent, LuiPosition.None, offset, shortname, skinId, name);

	public LuiContainer CreateItemIcon(string parent, LuiPosition position, LuiOffset offset, string shortname, ulong skinId = 0, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		ItemDefinition def = ItemManager.FindItemDefinition(shortname);
		if (def)
		{
			cont.SetAnchorAndOffset(position, offset);
			cont.SetItemIcon(def.itemid, skinId);
		}
		else
		{
			Interface.Oxide.LogWarning($"[LUI] We couldn't find '{shortname}' as valid item shortname. Ignoring.");
		}
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateItemIcon(LuiContainer container, LuiPosition position, LuiOffset offset, int itemId, ulong skinId = 0, string name = "") => CreateItemIcon(container.name, position, offset, itemId, skinId, name);
	public LuiContainer CreateItemIcon(LuiContainer container, LuiOffset offset, int itemId, ulong skinId = 0, string name = "") => CreateItemIcon(container.name, LuiPosition.None, offset, itemId, skinId, name);
	public LuiContainer CreateItemIcon(string parent, LuiOffset offset, int itemId, ulong skinId = 0, string name = "") => CreateItemIcon(parent, LuiPosition.None, offset, itemId, skinId, name);

	public LuiContainer CreateItemIcon(string parent, LuiPosition position, LuiOffset offset, int itemId, ulong skinId = 0, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetItemIcon(itemId, skinId);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateButton(LuiContainer container, LuiPosition position, LuiOffset offset, string command, string color, bool isProtected = true, string name = "") => CreateButton(container.name, position, offset, command, color, isProtected, name);
	public LuiContainer CreateButton(LuiContainer container, LuiOffset offset, string command, string color, bool isProtected = true, string name = "") => CreateButton(container.name, LuiPosition.None, offset, command, color, isProtected, name);
	public LuiContainer CreateButton(string parent, LuiOffset offset, string command, string color, bool isProtected = true, string name = "") => CreateButton(parent, LuiPosition.None, offset, command, color, isProtected, name);

	public LuiContainer CreateButton(string parent, LuiPosition position, LuiOffset offset, string command, string color, bool isProtected = true, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetButton(command, color);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateInput(LuiContainer container, LuiPosition position, LuiOffset offset, string color, string text, int fontSize, string command, int charLimit = 0, bool isProtected = true, CUI.Handler.FontTypes font = CUI.Handler.FontTypes.RobotoCondensedBold, TextAnchor alignment = TextAnchor.UpperLeft, string name = "") => CreateInput(container.name, position, offset, color, text, fontSize, command, charLimit, isProtected, font, alignment, name);
	public LuiContainer CreateInput(LuiContainer container, LuiOffset offset, string color, string text, int fontSize, string command, int charLimit = 0, bool isProtected = true, CUI.Handler.FontTypes font = CUI.Handler.FontTypes.RobotoCondensedBold, TextAnchor alignment = TextAnchor.UpperLeft, string name = "") => CreateInput(container.name, LuiPosition.None, offset, color, text, fontSize, command, charLimit, isProtected, font, alignment, name);
	public LuiContainer CreateInput(string parent, LuiOffset offset, string color, string text, int fontSize, string command, int charLimit = 0, bool isProtected = true, CUI.Handler.FontTypes font = CUI.Handler.FontTypes.RobotoCondensedBold, TextAnchor alignment = TextAnchor.UpperLeft, string name = "") => CreateInput(parent, LuiPosition.None, offset, color, text, fontSize, command, charLimit, isProtected, font, alignment, name);

	public LuiContainer CreateInput(string parent, LuiPosition position, LuiOffset offset, string color, string text, int fontSize, string command, int charLimit = 0, bool isProtected = true, CUI.Handler.FontTypes font = CUI.Handler.FontTypes.RobotoCondensedBold, TextAnchor alignment = TextAnchor.MiddleCenter, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetInput(color, text, fontSize, command, charLimit, font, alignment);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateCountdown(LuiContainer container, LuiPosition position, LuiOffset offset, int startTime, int endTime, float step = 1, float interval = 1, string command = null, bool isProtected = true, string name = "") => CreateCountdown(container.name, position, offset, startTime, endTime, step, interval, command, isProtected, name);
	public LuiContainer CreateCountdown(LuiContainer container, LuiOffset offset, int startTime, int endTime, float step = 1, float interval = 1, string command = null, bool isProtected = true, string name = "") => CreateCountdown(container.name, LuiPosition.None, offset, startTime, endTime, step, interval, command, isProtected, name);
	public LuiContainer CreateCountdown(string parent, LuiOffset offset, int startTime, int endTime, float step = 1, float interval = 1, string command = null, bool isProtected = true, string name = "") => CreateCountdown(parent, LuiPosition.None, offset, startTime, endTime, step, interval, command, isProtected, name);

	public LuiContainer CreateCountdown(string parent, LuiPosition position, LuiOffset offset, int startTime, int endTime, float step = 1, float interval = 1, string command = null, bool isProtected = true, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetCountdown(startTime, endTime, step, interval, command);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateDraggable(LuiContainer container, LuiPosition position, LuiOffset offset, string color, string filter = null, bool dropAnywhere = true, bool keepOnTop = false, bool limitToParent = false, float maxDistance = -1f, bool allowSwapping = false, string name = "") => CreateDraggable(container.name, position, offset, color, filter, dropAnywhere, keepOnTop, limitToParent, maxDistance, allowSwapping, name);
	public LuiContainer CreateDraggable(LuiContainer container, LuiOffset offset, string color, string filter = null, bool dropAnywhere = true, bool keepOnTop = false, bool limitToParent = false, float maxDistance = -1f, bool allowSwapping = false, string name = "") => CreateDraggable(container.name, LuiPosition.None, offset, color, filter, dropAnywhere, keepOnTop, limitToParent, maxDistance, allowSwapping, name);
	public LuiContainer CreateDraggable(string parent, LuiOffset offset, string color, string filter = null, bool dropAnywhere = true, bool keepOnTop = false, bool limitToParent = false, float maxDistance = -1f, bool allowSwapping = false, string name = "") => CreateDraggable(parent, LuiPosition.None, offset, color, filter, dropAnywhere, keepOnTop, limitToParent, maxDistance, allowSwapping, name);

	public LuiContainer CreateDraggable(string parent, LuiPosition position, LuiOffset offset, string color, string filter = null, bool dropAnywhere = true, bool keepOnTop = false, bool limitToParent = false, float maxDistance = -1f, bool allowSwapping = false, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetColor(color);
		cont.SetDraggable(filter, dropAnywhere, keepOnTop, limitToParent, maxDistance, allowSwapping);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateSlot(LuiContainer container, LuiPosition position, LuiOffset offset, string filter = null, string name = "") => CreateSlot(container.name, position, offset, filter, name);
	public LuiContainer CreateSlot(LuiContainer container, LuiOffset offset, string filter = null, string name = "") => CreateSlot(container.name, LuiPosition.None, offset, filter, name);
	public LuiContainer CreateSlot(string parent, LuiOffset offset, string filter = null, string name = "") => CreateSlot(parent, LuiPosition.None, offset, filter, name);

	public LuiContainer CreateSlot(string parent, LuiPosition position, LuiOffset offset, string filter = null, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetSlot(filter);
		elements.Add(cont);
		return cont;
	}

	public LuiContainer CreateScrollView(LuiContainer container, LuiPosition position, LuiOffset offset, bool vertical, bool horizontal, ScrollRect.MovementType movementType = ScrollRect.MovementType.Clamped, float elasticity = 0, bool inertia = false, float decelerationRate = 0, float scrollSensitivity = 0, LuiScrollbar verticalScrollOptions = default, LuiScrollbar horizontalScrollOptions = default, string name = "") => CreateScrollView(container.name, position, offset, vertical, horizontal, movementType, elasticity, inertia, decelerationRate, scrollSensitivity, verticalScrollOptions, horizontalScrollOptions, name);
	public LuiContainer CreateScrollView(LuiContainer container, LuiOffset offset, bool vertical, bool horizontal, ScrollRect.MovementType movementType = ScrollRect.MovementType.Clamped, float elasticity = 0, bool inertia = false, float decelerationRate = 0, float scrollSensitivity = 0, LuiScrollbar verticalScrollOptions = default, LuiScrollbar horizontalScrollOptions = default, string name = "") => CreateScrollView(container.name, LuiPosition.None, offset, vertical, horizontal, movementType, elasticity, inertia, decelerationRate, scrollSensitivity, verticalScrollOptions, horizontalScrollOptions, name);
	public LuiContainer CreateScrollView(string parent, LuiOffset offset, bool vertical, bool horizontal, ScrollRect.MovementType movementType = ScrollRect.MovementType.Clamped, float elasticity = 0, bool inertia = false, float decelerationRate = 0, float scrollSensitivity = 0, LuiScrollbar verticalScrollOptions = default, LuiScrollbar horizontalScrollOptions = default, string name = "") => CreateScrollView(parent, LuiPosition.None, offset, vertical, horizontal, movementType, elasticity, inertia, decelerationRate, scrollSensitivity, verticalScrollOptions, horizontalScrollOptions, name);

	public LuiContainer CreateScrollView(string parent, LuiPosition position, LuiOffset offset, bool vertical, bool horizontal, ScrollRect.MovementType movementType = ScrollRect.MovementType.Clamped, float elasticity = 0, bool inertia = false, float decelerationRate = 0, float scrollSensitivity = 0, LuiScrollbar verticalScrollOptions = default, LuiScrollbar horizontalScrollOptions = default, string name = "")
	{
		LuiContainer cont = CreateEmptyContainer(parent, name);
		cont.SetAnchorAndOffset(position, offset);
		cont.SetScrollView(vertical, horizontal, movementType, elasticity, inertia, decelerationRate, scrollSensitivity, verticalScrollOptions, horizontalScrollOptions);
		elements.Add(cont);
		return cont;
	}

	#endregion

	/// <summary>
	/// Builds and sends UI to player.
	/// </summary>
	public void SendUi(BasePlayer player) => SendJson(new SendInfo(player.net.connection));

	/// <summary>
	/// Builds and sends UI to player. Preffered SendUi(BasePlayer) over this method.
	/// </summary>
	public void SendUiJson(BasePlayer player) => SendJson(new SendInfo(player.net.connection));

	/// <summary>
	/// Returns string JSON of currently builded UI.
	/// </summary>
	public string ToJson()
	{
		LUIBuilder cbi = new LUIBuilder(this);
		return cbi.GetJsonString();
	}

	private void SendJson(SendInfo send)
	{
		LUIBuilder cbi = new LUIBuilder(this);
		NetWrite write = Net.sv.StartWrite();
		write.PacketID(Message.Type.RPCMessage);
		write.EntityID(CommunityEntity.ServerInstance.net.ID);
		write.UInt32(StringPool.Get("AddUI"));
		write.String(cbi.GetJsonString());
		write.Send(send);
	}

	public void Dispose()
	{
		elements.Clear();
	}

	public class LuiContainer
	{
		public string name;
		public string parent;
		public LuiComponentDictionary luiComponents = new();
		public string destroyUi;
		public float fadeOut;
		public bool update;

		#region Container Methods - Global

		public LuiContainer SetDestroy(string name)
		{
			destroyUi = name;
			return this;
		}

		public LuiContainer SetFadeOut(float time)
		{
			fadeOut = time;
			return this;
		}

		public LuiContainer SetName(string newName)
		{
			name = newName;
			return this;
		}

	    /// <summary>
	    /// Updates or creates new component in current element.
		/// Recommended to use only when there is no built-in method for that.
	    /// </summary>
		public T UpdateComp<T>() where T : LuiCompBase, new()
		{
			//if (!update)
			//	Logger.Warn($"[LUI] You're trying to create update in element '{name}' (of parent '{parent}') which doesn't allow updates. Ignoring.");
			if (luiComponents.TryGetValue<T>(GetLuiCompType(typeof(T)), out var component))
			{
				return component;
			}
			component = new T();
			luiComponents.Add(component.type, component);
			return component;
		}

		public void SetEnabled<T>(bool enabled = true) where T : LuiCompBase
		{
			//if (!update)
			//{
			//	Logger.Warn($"[LUI] You're trying to create update in element '{name}' (of parent '{parent}') which doesn't allow updates. Ignoring.");
			//	return;
			//}
			if (luiComponents.TryGetValue<T>(GetLuiCompType(typeof(T)), out var component))
			{
				component.enabled = enabled;
			}
			else
			{
				Interface.Oxide.LogWarning($"[LUI] You're trying to switch state of component '{typeof(T)}' but it isn't present. Ignoring.");
			}
		}

		public void SetFadeIn<T>(float fadeIn) where T : LuiCompBase
		{
			if (luiComponents.TryGetValue<T>(GetLuiCompType(typeof(T)), out var component))
			{
				component.fadeIn = fadeIn;
			}
			else
			{
				Interface.Oxide.LogWarning($"[LUI] You're trying to switch fadeIn of component '{typeof(T)}' but it isn't present. Ignoring.");
			}
		}

		public static LuiCompType GetLuiCompType(Type type)
		{
			return type switch
			{
				not null when type == typeof(LuiTextComp) => LuiCompType.Text,
				not null when type == typeof(LuiImageComp) => LuiCompType.Image,
				not null when type == typeof(LuiRawImageComp) => LuiCompType.RawImage,
				not null when type == typeof(LuiButtonComp) => LuiCompType.Button,
				not null when type == typeof(LuiOutlineComp) => LuiCompType.Outline,
				not null when type == typeof(LuiInputComp) => LuiCompType.InputField,
				not null when type == typeof(LuiCursorComp) => LuiCompType.NeedsCursor,
				not null when type == typeof(LuiRectTransformComp) => LuiCompType.RectTransform,
				not null when type == typeof(LuiCountdownComp) => LuiCompType.Countdown,
				not null when type == typeof(LuiDraggableComp) => LuiCompType.Draggable,
				not null when type == typeof(LuiSlotComp) => LuiCompType.Slot,
				not null when type == typeof(LuiKeyboardComp) => LuiCompType.NeedsKeyboard,
				not null when type == typeof(LuiScrollComp) => LuiCompType.ScrollView,
				_ => LuiCompType.Image
			};
		}

		#endregion

		#region Container Methods - LuiTextComp

		public LuiContainer SetText(string input, int fontSize = 0, string color = null, TextAnchor alignment = TextAnchor.MiddleCenter, bool update = false)
		{
			if (luiComponents.TryGetValue<LuiTextComp>(LuiCompType.Text, out var text))
			{
				text.text = input;
				if (fontSize > 0)
					text.fontSize = fontSize;
				if (color != null)
					text.color = color;
				if (!update)
					text.align = nameof(alignment);
			}
			else
			{
				text = new();
				text.text = input;
				if (fontSize > 0)
					text.fontSize = fontSize;
				if (color != null)
					text.color = color;
				if (!update)
					text.align = nameof(alignment);
				luiComponents.Add(text.type, text);
			}
			return this;
		}

		public LuiContainer SetTextColor(string color)
		{
			if (luiComponents.TryGetValue<LuiTextComp>(LuiCompType.Text, out var text))
			{
				text.color = color;
			}
			else
			{
				text = new();
				text.color = color;
				luiComponents.Add(text.type, text);
			}
			return this;
		}

		public LuiContainer SetTextFont(CUI.Handler.FontTypes font)
		{
			if (luiComponents.TryGetValue<LuiTextComp>(LuiCompType.Text, out var text))
			{
				text.font = nameof(font);
			}
			else
			{
				text = new();
				text.font = nameof(font);
				luiComponents.Add(text.type, text);
			}
			return this;
		}

		public LuiContainer SetTextAlign(TextAnchor align)
		{
			if (luiComponents.TryGetValue<LuiTextComp>(LuiCompType.Text, out var text))
			{
				text.align = nameof(align);
			}
			else
			{
				text = new();
				text.align = nameof(align);
				luiComponents.Add(text.type, text);
			}
			return this;
		}

		public LuiContainer SetTextOverflow(VerticalWrapMode verticalOverflow)
		{
			if (luiComponents.TryGetValue<LuiTextComp>(LuiCompType.Text, out var text))
			{
				text.verticalOverflow = nameof(verticalOverflow);
			}
			else
			{
				text = new();
				text.verticalOverflow = nameof(verticalOverflow);
				luiComponents.Add(text.type, text);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiImageComp

		public LuiContainer SetColor(string color)
		{
			if (luiComponents.TryGetValue<LuiImageComp>(LuiCompType.Image, out var img))
			{
				img.color = color;
			}
			else
			{
				img = new();
				img.color = color;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		public LuiContainer SetMaterial(string material)
		{
			if (luiComponents.TryGetValue<LuiImageComp>(LuiCompType.Image, out var img))
			{
				img.material = material;
			}
			else
			{
				img = new();
				img.material = material;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		public LuiContainer SetSprite(string sprite = null, string color = null, UnityEngine.UI.Image.Type imageType = Image.Type.Simple)
		{
			if (luiComponents.TryGetValue<LuiImageComp>(LuiCompType.Image, out var img))
			{
				if (sprite != null)
				{
					img.sprite = sprite;
					img.imageType = nameof(imageType);
				}
				if (color != null)
					img.color = color;
			}
			else
			{
				img = new();
				if (sprite != null)
				{
					img.sprite = sprite;
					img.imageType = nameof(imageType);
				}
				if (color != null)
					img.color = color;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		public LuiContainer SetImage(uint png = 0, string color = null)
		{
			if (luiComponents.TryGetValue<LuiImageComp>(LuiCompType.Image, out var img))
			{
				if (png != 0)
					img.png = png;
				if (color != null)
					img.color = color;
			}
			else
			{
				img = new();
				if (png != 0)
					img.png = png;
				if (color != null)
					img.color = color;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		public LuiContainer SetItemIcon(int itemid, ulong skinid)
		{
			if (luiComponents.TryGetValue<LuiImageComp>(LuiCompType.Image, out var img))
			{
				img.itemid = itemid;
				img.skinid = skinid;
			}
			else
			{
				img = new();
				img.itemid = itemid;
				img.skinid = skinid;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiRawImageComp

		public LuiContainer SetUrlImage(string url = null, string color = null)
		{
			if (luiComponents.TryGetValue<LuiRawImageComp>(LuiCompType.RawImage, out var img))
			{
				if (url != null)
					img.url = url;
				if (color != null)
					img.color = color;
			}
			else
			{
				img = new();
				if (url != null)
					img.url = url;
				if (color != null)
					img.color = color;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		public LuiContainer SetSteamIcon(ulong steamid, string color = null)
		{
			if (luiComponents.TryGetValue<LuiRawImageComp>(LuiCompType.RawImage, out var img))
			{
				img.steamid = steamid;
				if (color != null)
					img.color = color;
			}
			else
			{
				img = new();
				img.steamid = steamid;
				if (color != null)
					img.color = color;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		public LuiContainer SetRawSprite(string sprite, string color = null)
		{
			if (luiComponents.TryGetValue<LuiRawImageComp>(LuiCompType.RawImage, out var img))
			{
				img.sprite = sprite;
				if (color != null)
					img.color = color;
			}
			else
			{
				img = new();
				img.sprite = sprite;
				if (color != null)
					img.color = color;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		public LuiContainer SetRawMaterial(string material, string color = null)
		{
			if (luiComponents.TryGetValue<LuiRawImageComp>(LuiCompType.RawImage, out var img))
			{
				img.material = material;
				if (color != null)
					img.color = color;
			}
			else
			{
				img = new();
				img.material = material;
				if (color != null)
					img.color = color;
				luiComponents.Add(img.type, img);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiButtonComp

		public LuiContainer SetButton(string command = null, string color = null)
		{
			if (luiComponents.TryGetValue<LuiButtonComp>(LuiCompType.Button, out var button))
			{
				if (command != null)
					button.command = command;
				if (color != null)
					button.color = color;
			}
			else
			{
				button = new();
				if (command != null)
					button.command = command;
				if (color != null)
					button.color = color;
				luiComponents.Add(button.type, button);
			}
			return this;
		}

		public LuiContainer SetButtonColors(string color = null, string normalColor = null, string highlightedColor = null, string pressedColor = null, string selectedColor = null, string disabledColor = null, float colorMultiplier = -1, float fadeDuration = -1)
		{
			if (luiComponents.TryGetValue<LuiButtonComp>(LuiCompType.Button, out var button))
			{
				if (color != null)
					button.color = color;
				if (normalColor != null)
					button.normalColor = normalColor;
				if (highlightedColor != null)
					button.highlightedColor = highlightedColor;
				if (pressedColor != null)
					button.pressedColor = pressedColor;
				if (selectedColor != null)
					button.selectedColor = selectedColor;
				if (disabledColor != null)
					button.disabledColor = disabledColor;
				if (colorMultiplier != -1)
					button.colorMultiplier = colorMultiplier;
				if (fadeDuration != -1)
					button.fadeDuration = fadeDuration;
			}
			else
			{
				button = new();
				if (color != null)
					button.color = color;
				if (normalColor != null)
					button.normalColor = normalColor;
				if (highlightedColor != null)
					button.highlightedColor = highlightedColor;
				if (pressedColor != null)
					button.pressedColor = pressedColor;
				if (selectedColor != null)
					button.selectedColor = selectedColor;
				if (disabledColor != null)
					button.disabledColor = disabledColor;
				if (colorMultiplier != -1)
					button.colorMultiplier = colorMultiplier;
				if (fadeDuration != -1)
					button.fadeDuration = fadeDuration;
				luiComponents.Add(button.type, button);
			}
			return this;
		}

		public LuiContainer SetButtonMaterial(string material)
		{
			if (luiComponents.TryGetValue<LuiButtonComp>(LuiCompType.Button, out var button))
			{
				button.material = material;
			}
			else
			{
				button = new();
				button.material = material;
				luiComponents.Add(button.type, button);
			}
			return this;
		}

		public LuiContainer SetButtonSprite(string sprite,  UnityEngine.UI.Image.Type imageType = Image.Type.Simple)
		{
			if (luiComponents.TryGetValue<LuiButtonComp>(LuiCompType.Button, out var button))
			{
				button.sprite = sprite;
				button.imageType = nameof(imageType);
			}
			else
			{
				button = new();
				button.sprite = sprite;
				button.imageType = nameof(imageType);
				luiComponents.Add(button.type, button);
			}
			return this;
		}

		public LuiContainer SetButtonClose(string close)
		{
			if (luiComponents.TryGetValue<LuiButtonComp>(LuiCompType.Button, out var button))
			{
				button.close = close;
			}
			else
			{
				button = new();
				button.close = close;
				luiComponents.Add(button.type, button);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiOutlineComp

		public LuiContainer SetOutline(string color, Vector2 distance, bool useGraphicAlpha = false)
		{
			if (luiComponents.TryGetValue<LuiOutlineComp>(LuiCompType.Outline, out var outline))
			{
				outline.color = color;
				outline.distance = distance;
				outline.useGraphicAlpha = useGraphicAlpha;
			}
			else
			{
				outline = new();
				outline.color = color;
				outline.distance = distance;
				outline.useGraphicAlpha = useGraphicAlpha;
				luiComponents.Add(outline.type, outline);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiInputComp

		public LuiContainer SetInput(string color = null, string text = null, int fontSize = 0, string command = null, int charLimit = 0, CUI.Handler.FontTypes font = CUI.Handler.FontTypes.RobotoCondensedBold, TextAnchor alignment = TextAnchor.MiddleCenter, bool update = false)
		{
			if (luiComponents.TryGetValue<LuiInputComp>(LuiCompType.InputField, out var input))
			{
				if (color != null)
					input.color = color;
				if (text != null)
					input.text = text;
				if (fontSize > 0)
					input.fontSize = fontSize;
				if (command != null)
					input.command = command;
				if (charLimit > 0)
					input.characterLimit = charLimit;
				if (!update)
				{
					input.align = nameof(alignment);
					input.font = nameof(font);
				}
			}
			else
			{
				input = new();
				if (color != null)
					input.color = color;
				if (text != null)
					input.text = text;
				if (fontSize > 0)
					input.fontSize = fontSize;
				if (command != null)
					input.command = command;
				if (charLimit > 0)
					input.characterLimit = charLimit;
				if (!update)
				{
					input.align = nameof(alignment);
					input.font = nameof(font);
				}
				luiComponents.Add(input.type, input);
			}
			return this;
		}

		public LuiContainer SetInputReadOnly(bool readOnly)
		{
			if (luiComponents.TryGetValue<LuiInputComp>(LuiCompType.InputField, out var input))
			{
				input.readOnly = readOnly;
			}
			else
			{
				input = new();
				input.readOnly = readOnly;
				luiComponents.Add(input.type, input);
			}
			return this;
		}

		public LuiContainer SetInputPassword(bool password)
		{
			if (luiComponents.TryGetValue<LuiInputComp>(LuiCompType.InputField, out var input))
			{
				input.password = password;
			}
			else
			{
				input = new();
				input.password = password;
				luiComponents.Add(input.type, input);
			}
			return this;
		}

		public LuiContainer SetInputAutoFocus(bool autofocus)
		{
			if (luiComponents.TryGetValue<LuiInputComp>(LuiCompType.InputField, out var input))
			{
				input.autofocus = autofocus;
			}
			else
			{
				input = new();
				input.autofocus = autofocus;
				luiComponents.Add(input.type, input);
			}
			return this;
		}

		public LuiContainer SetInputKeyboard(bool needsKeyboard = false, bool hudMenuInput = false)
		{
			if (luiComponents.TryGetValue<LuiInputComp>(LuiCompType.InputField, out var input))
			{
				input.needsKeyboard = needsKeyboard;
				input.hudMenuInput = hudMenuInput;
			}
			else
			{
				input = new();
				input.needsKeyboard = needsKeyboard;
				input.hudMenuInput = hudMenuInput;
				luiComponents.Add(input.type, input);
			}
			return this;
		}

		public LuiContainer SetInputLineType(UnityEngine.UI.InputField.LineType lineType)
		{
			if (luiComponents.TryGetValue<LuiInputComp>(LuiCompType.InputField, out var input))
			{
				input.lineType = nameof(lineType);
			}
			else
			{
				input = new();
				input.lineType = nameof(lineType);
				luiComponents.Add(input.type, input);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiCursorComp

		public LuiContainer AddCursor()
		{
			if (!luiComponents.TryGetValue<LuiCursorComp>(LuiCompType.Button, out var cursor))
			{
				cursor = new();
				luiComponents.Add(cursor.type, cursor);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiRectTransformComp

		public LuiContainer SetAnchors(LuiPosition pos)
		{
			if (luiComponents.TryGetValue<LuiRectTransformComp>(LuiCompType.RectTransform, out var rect))
			{
				rect.anchor = pos;
			}
			else
			{
				rect = new();
				rect.anchor = pos;
				luiComponents.Add(rect.type, rect);
			}
			return this;
		}

		public LuiContainer SetOffset(LuiOffset off)
		{
			if (luiComponents.TryGetValue<LuiRectTransformComp>(LuiCompType.RectTransform, out var rect))
			{
				rect.offset = off;
			}
			else
			{
				rect = new();
				rect.offset = off;
				luiComponents.Add(rect.type, rect);
			}
			return this;
		}

		public LuiContainer SetAnchorAndOffset(LuiPosition pos, LuiOffset off)
		{
			if (luiComponents.TryGetValue<LuiRectTransformComp>(LuiCompType.RectTransform, out var rect))
			{
				rect.anchor = pos;
				rect.offset = off;
			}
			else
			{
				rect = new();
				rect.anchor = pos;
				rect.offset = off;
				luiComponents.Add(rect.type, rect);
			}
			return this;
		}

		public LuiContainer SetRectParent(string setParent)
		{
			if (luiComponents.TryGetValue<LuiRectTransformComp>(LuiCompType.RectTransform, out var rect))
			{
				rect.setParent = setParent;
			}
			else
			{
				rect = new();
				rect.setParent = setParent;
				luiComponents.Add(rect.type, rect);
			}
			return this;
		}

		public LuiContainer SetRectIndex(int setTransformIndex)
		{
			if (luiComponents.TryGetValue<LuiRectTransformComp>(LuiCompType.RectTransform, out var rect))
			{
				rect.setTransformIndex = setTransformIndex;
			}
			else
			{
				rect = new();
				rect.setTransformIndex = setTransformIndex;
				luiComponents.Add(rect.type, rect);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiCountdownComp

		public LuiContainer SetCountdown(int startTime, int endTime, float step = 1, float interval = 1, string command = null)
		{
			if (luiComponents.TryGetValue<LuiCountdownComp>(LuiCompType.Countdown, out var countdown))
			{
				countdown.startTime = startTime;
				countdown.endTime = endTime;
				if (step != 1)
					countdown.step = step;
				if (interval != 1)
					countdown.interval = interval;
				if (command != null)
					countdown.command = command;
			}
			else
			{
				countdown = new();
				countdown.startTime = startTime;
				countdown.endTime = endTime;
				if (step != 1)
					countdown.step = step;
				if (interval != 1)
					countdown.interval = interval;
				if (command != null)
					countdown.command = command;
				luiComponents.Add(countdown.type, countdown);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiDraggableComp

		public LuiContainer SetDraggable(string filter = null, bool dropAnywhere = true, bool keepOnTop = false, bool limitToParent = false, float maxDistance = -1f, bool allowSwapping = false)
		{
			if (luiComponents.TryGetValue<LuiDraggableComp>(LuiCompType.Draggable, out var drag))
			{
				if (filter != null)
					drag.filter = filter;
				if (drag.maxDistance != -1)
					drag.maxDistance = maxDistance;
				drag.dropAnywhere = dropAnywhere;
				drag.keepOnTop = keepOnTop;
				drag.limitToParent = limitToParent;
				drag.allowSwapping = allowSwapping;
			}
			else
			{
				drag = new();
				if (filter != null)
					drag.filter = filter;
				if (drag.maxDistance != -1)
					drag.maxDistance = maxDistance;
				drag.dropAnywhere = dropAnywhere;
				drag.keepOnTop = keepOnTop;
				drag.limitToParent = limitToParent;
				drag.allowSwapping = allowSwapping;
				luiComponents.Add(drag.type, drag);
			}
			return this;
		}
		#endregion

		#region Container Methods - LuiSlotComp

		public LuiContainer SetSlot(string filter = null)
		{
			if (luiComponents.TryGetValue<LuiSlotComp>(LuiCompType.Slot, out var slot))
			{
				if (filter != null)
					slot.filter = filter;
			}
			else
			{
				slot = new();
				if (filter != null)
					slot.filter = filter;
				luiComponents.Add(slot.type, slot);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiKeyboardComp

		public LuiContainer AddKeyboard()
		{
			if (!luiComponents.TryGetValue<LuiKeyboardComp>(LuiCompType.Button, out var keyboard))
			{
				keyboard = new();
				luiComponents.Add(keyboard.type, keyboard);
			}
			return this;
		}

		#endregion

		#region Container Methods - LuiScrollComp

		public LuiContainer SetScrollView(bool vertical, bool horizontal, ScrollRect.MovementType movementType = ScrollRect.MovementType.Clamped, float elasticity = 0, bool inertia = false, float decelerationRate = 0, float scrollSensitivity = 0, LuiScrollbar verticalScrollOptions = default, LuiScrollbar horizontalScrollOptions = default, bool update = false)
		{
			if (luiComponents.TryGetValue<LuiScrollComp>(LuiCompType.ScrollView, out var scroll))
			{
				if (!update)
				{
					scroll.vertical = vertical;
					scroll.horizontal = horizontal;
					scroll.movementType = nameof(movementType);
					scroll.inertia = inertia;
				}
				if (elasticity != 0)
					scroll.elasticity = elasticity;
				if (decelerationRate != 0)
					scroll.decelerationRate = decelerationRate;
				if (scrollSensitivity != 0)
					scroll.scrollSensitivity = scrollSensitivity;
				scroll.verticalScrollbar = verticalScrollOptions;
				scroll.horizontalScrollbar = horizontalScrollOptions;
			}
			else
			{
				scroll = new();
				if (!update)
				{
					scroll.vertical = vertical;
					scroll.horizontal = horizontal;
					scroll.movementType = nameof(movementType);
					scroll.inertia = inertia;
				}
				if (elasticity != 0)
					scroll.elasticity = elasticity;
				if (decelerationRate != 0)
					scroll.decelerationRate = decelerationRate;
				if (scrollSensitivity != 0)
					scroll.scrollSensitivity = scrollSensitivity;
				scroll.verticalScrollbar = verticalScrollOptions;
				scroll.horizontalScrollbar = horizontalScrollOptions;
				luiComponents.Add(scroll.type, scroll);
			}
			return this;
		}

		public LuiContainer SetScrollContent(LuiPosition pos, LuiOffset offset)
		{
			if (luiComponents.TryGetValue<LuiScrollComp>(LuiCompType.ScrollView, out var scroll))
			{
				scroll.anchor = pos;
				scroll.offset = offset;
			}
			else
			{
				scroll.anchor = pos;
				scroll.offset = offset;
				luiComponents.Add(scroll.type, scroll);
			}
			return this;
		}

		#endregion

	}
}

public static class LuiColors
{
	public const string Transparent = "0.0 0.0 0.0 0.0";
	public const string White = "1.0 1.0 1.0 1.0";
	public const string Gray = "0.5 0.5 0.5 1.0";
	public const string Black = "0.0 0.0 0.0 1.0";
	public const string Red = "1.0 0.0 0.0 1.0";
	public const string Green = "0.0 1.0 0.0 1.0";
	public const string Blue = "0.0 0.0 1.0 1.0";
}

public class LuiComponentDictionary : IEnumerable
{
	private readonly LuiCompBase[] _values;
	private int _count;
	private const int DictionarySize = 10; //Dictionary based on most possible types of components in one element, at the date od 24.02.2025 it's 8 (including draggables), so adding 2 more for safety.

	public LuiComponentDictionary()
	{
		_values = new LuiCompBase[DictionarySize];
		_count = 0;
	}

	public int Count => _count;

	public void Add<T>(LuiCompType key, T value) where T : LuiCompBase
	{
		if (_count >= _values.Length)
			throw new InvalidOperationException("Dictionary is full");

		_values[_count] = value;
		_count++;
	}

	public void Clear()
	{
		_count = 0;
	}

	public bool TryGetValue<T>(LuiCompType key, out T value) where T : LuiCompBase
	{
		for (int i = 0; i < _count; i++)
		{
			if (_values[i].type == key && _values[i] is T typedValue)
			{
				value = typedValue;
				return true;
			}
		}
		value = null;
		return false;
	}

	public IEnumerator GetEnumerator()
	{
		for (int i = 0; i < _count; i++)
		{
			yield return _values[i];
		}
	}
}

public readonly struct LuiOffset
{
	public static readonly LuiOffset None = new(0, 0, 0, 0);

	public readonly Vector2 offsetMin;
	public readonly Vector2 offsetMax;

	public LuiOffset(float xMin, float yMin, float xMax, float yMax)
	{
		offsetMin = new Vector2(xMin, yMin);
		offsetMax = new Vector2(xMax, yMax);
	}

	public static bool operator ==(LuiOffset a, LuiOffset b)
	{
		return a.offsetMax == b.offsetMax && a.offsetMin == b.offsetMin;
	}

	public static bool operator !=(LuiOffset a, LuiOffset b)
	{
		return a.offsetMax != b.offsetMax || a.offsetMin != b.offsetMin;
	}

	public override bool Equals(object obj)
	{
		return obj is LuiOffset other && Equals(other);
	}

	private bool Equals(LuiOffset other)
	{
		return offsetMin.Equals(other.offsetMin) && offsetMax.Equals(other.offsetMax);
	}

	public override int GetHashCode()
	{
		unchecked
		{
			int hash = 17;
			hash = hash * 31 + offsetMin.GetHashCode();
			hash = hash * 31 + offsetMax.GetHashCode();
			return hash;
		}
	}
}

public readonly struct LuiPosition
{
	public static readonly LuiPosition None = new(0, 0, 0, 0);
	public static readonly LuiPosition Full = new(0, 0, 1, 1);
	public static readonly LuiPosition UpperLeft = new(0, 1, 0, 1);
	public static readonly LuiPosition UpperCenter = new(.5f, 1, .5f, 1);
	public static readonly LuiPosition UpperRight = new(1, 1, 1, 1);
	public static readonly LuiPosition MiddleLeft = new(0, .5f, 0, .5f);
	public static readonly LuiPosition MiddleCenter = new(.5f, .5f, .5f, .5f);
	public static readonly LuiPosition MiddleRight = new(1, .5f, 1, .5f);
	public static readonly LuiPosition LowerLeft = new(0, 0, 0, 0);
	public static readonly LuiPosition LowerCenter = new(.5f, 0, .5f, 0);
	public static readonly LuiPosition LowerRight = new(1, 0, 1, 0);

	public readonly Vector2 anchorMin;
	public readonly Vector2 anchorMax;

	public LuiPosition(float xMin, float yMin, float xMax, float yMax)
	{
		anchorMin = new Vector2(xMin, yMin);
		anchorMax = new Vector2(xMax, yMax);
	}

	public static bool operator ==(LuiPosition a, LuiPosition b)
	{
		return a.anchorMax == b.anchorMax && a.anchorMin == b.anchorMin;
	}

	public static bool operator !=(LuiPosition a, LuiPosition b)
	{
		return a.anchorMax != b.anchorMax || a.anchorMin != b.anchorMin;
	}

	public override bool Equals(object obj)
	{
		return obj is LuiPosition other && Equals(other);
	}

	private bool Equals(LuiPosition other)
	{
		return anchorMax.Equals(other.anchorMax) && anchorMin.Equals(other.anchorMin);
	}

	public override int GetHashCode()
	{
		unchecked
		{
			int hash = 17;
			hash = hash * 31 + anchorMax.GetHashCode();
			hash = hash * 31 + anchorMin.GetHashCode();
			return hash;
		}
	}
}

public enum LuiCompType
{
	Text = 0,
	Image = 1,
	RawImage = 2,
	Button = 3,
	Outline = 4,
	InputField = 5,
	NeedsCursor = 6,
	RectTransform = 7,
	Countdown = 8,
	Draggable = 9,
	Slot = 10,
	NeedsKeyboard = 11,
	ScrollView = 12,
}

public class LuiCompBase
{
	public LuiCompType type;
	public bool enabled = true;
	public float fadeIn; //Present in like 80% of elements but to reduce method list, adding it here.
}

public class LuiTextComp : LuiCompBase
{
	public string text;
	public int fontSize;
	public string font;
	public string align;
	public string color;
	public string verticalOverflow;

	public LuiTextComp()
	{
		type = LuiCompType.Text;
	}
}

public class LuiImageComp : LuiCompBase
{
	public string sprite;
	public string material;
	public string color;
	public string imageType;
	public uint png;
	public int itemid;
	public ulong skinid;

	public LuiImageComp()
	{
		type = LuiCompType.Image;
	}
}

public class LuiRawImageComp : LuiCompBase
{
	public string sprite;
	public string color;
	public string material;
	public string url;
	public ulong steamid;

	public LuiRawImageComp()
	{
		type = LuiCompType.RawImage;
	}
}

public class LuiButtonComp : LuiCompBase
{
	public string command;
	public string close;
	public string sprite;
	public string material;
	public string color;
	public string imageType;
	public string normalColor;
	public string highlightedColor;
	public string pressedColor;
	public string selectedColor;
	public string disabledColor;
	public float colorMultiplier = -1;
	public float fadeDuration = -1;

	public LuiButtonComp()
	{
		type = LuiCompType.Button;
	}
}

public class LuiOutlineComp : LuiCompBase
{
	public string color;
	public Vector2 distance;
	public bool useGraphicAlpha;

	public LuiOutlineComp()
	{
		type = LuiCompType.Outline;
	}
}

public class LuiInputComp : LuiCompBase
{
	public int fontSize;
	public string font;
	public string align;
	public string color;
	public int characterLimit;
	public string command;
	public string text;
	public bool readOnly;
	public string lineType;
	public bool password;
	public bool needsKeyboard;
	public bool hudMenuInput;
	public bool autofocus;

	public LuiInputComp()
	{
		type = LuiCompType.InputField;
	}
}

public class LuiCursorComp : LuiCompBase
{
	public LuiCursorComp()
	{
		type = LuiCompType.NeedsCursor;
	}
}

public class LuiRectTransformComp : LuiCompBase
{
	public LuiPosition anchor = LuiPosition.Full;
	public LuiOffset offset = LuiOffset.None;
	public string setParent;
	public int setTransformIndex = -1;

	public LuiRectTransformComp()
	{
		type = LuiCompType.RectTransform;
	}
}

public class LuiCountdownComp : LuiCompBase
{
	public float endTime = -1;
	public float startTime = -1;
	public float step;
	public float interval;
	public string timerFormat;
	public string numberFormat;
	public bool destroyIfDone = true;
	public string command;

	public LuiCountdownComp()
	{
		type = LuiCompType.Countdown;
	}
}

public class LuiDraggableComp : LuiCompBase
{
	public bool limitToParent;
	public float maxDistance;
	public bool allowSwapping;
	public bool dropAnywhere = true;
	public float dragAlpha = -1;
	public int parentLimitIndex = -1;
	public string filter;
	public Vector2 parentPadding;
	public Vector2 anchorOffset;
	public bool keepOnTop;
	public string positionRPC;
	public bool moveToAnchor;
	public bool rebuildAnchor;

	public LuiDraggableComp()
	{
		type = LuiCompType.Draggable;
	}
}

public class LuiSlotComp : LuiCompBase
{
	public string filter;

	public LuiSlotComp()
	{
		type = LuiCompType.Slot;
	}
}

public class LuiKeyboardComp : LuiCompBase
{
	public LuiKeyboardComp()
	{
		type = LuiCompType.NeedsKeyboard;
	}
}

public class LuiScrollComp : LuiCompBase
{
	public LuiPosition anchor = LuiPosition.Full;
	public LuiOffset offset = LuiOffset.None;
	public bool horizontal;
	public bool vertical;
	public string movementType;
	public float elasticity = -1;
	public bool inertia;
	public float decelerationRate = -1;
	public float scrollSensitivity = -1;
	public LuiScrollbar horizontalScrollbar;
	public LuiScrollbar verticalScrollbar;


	public LuiScrollComp()
	{
		type = LuiCompType.ScrollView;
	}
}

public struct LuiScrollbar
{
	public bool disabled; //reverse of enabled
	public bool invert;
	public bool autoHide;
	public string handleSprite;
	public float size;
	public string handleColor;
	public string highlightColor;
	public string pressedColor;
	public string trackSprite;
	public string trackColor;
}