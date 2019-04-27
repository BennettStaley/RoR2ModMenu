using System;
using System.Collections.Generic;
using UnityEngine;
using RoR2;

namespace RoRCheats
{
    public class Main : MonoBehaviour
    {
        private static RoR2.CharacterMaster LocalPlayer;
        private static RoR2.Inventory LocalPlayerInv;
        private static RoR2.HealthComponent LocalHealth;
        private static RoR2.SkillLocator LocalSkills;
        private static RoR2.NetworkUser LocalNetworkUser;
        private static bool _isMenuOpen = false;
        private static bool _ifDragged = false;
        private static bool _CharacterCollected = false;

        public static GUIStyle BgStyle, OnStyle, OffStyle, LabelStyle, BtnStyle, CornerStyle;
        public static GUIStyle BtnStyle1, BtnStyle2, BtnStyle3;
        public static bool skillToggle, renderInteractables; 
        public static float delay = 0, widthSize = 600;

        public static Rect rect = new Rect(10, 10, 20, 20);
        public static Texture2D Image = null, ontexture, onpresstexture, offtexture, offpresstexture, cornertexture, backtexture, btntexture, btnpresstexture;
        public static Texture2D NewTexture2D { get { return new Texture2D(1, 1); } }
        public static int itemsToRoll = 10;
        public static int allItemsQuantity = 5;
        private static ulong xpToGive = 100;
        public static uint moneyToGive = 100;
        public static uint coinsToGive = 10;
        public static int btnY, mulY;
        public static string str = "0";


        private void OnGUI()
        {
            
            rect = GUI.Window(0, rect, new GUI.WindowFunction(SetBG), "", new GUIStyle());
            if (_isMenuOpen)
            {
                DrawMenu();
            }
            if (_CharacterCollected)
            {
                ESPRoutine();
            }
        }

        public void Start()
        {
            if (BgStyle == null)
            {
                BgStyle = new GUIStyle();
                BgStyle.normal.background = BackTexture;
                BgStyle.onNormal.background = BackTexture;
                BgStyle.active.background = BackTexture;
                BgStyle.onActive.background = BackTexture;
                BgStyle.normal.textColor = Color.white;
                BgStyle.onNormal.textColor = Color.white;
                BgStyle.active.textColor = Color.white;
                BgStyle.onActive.textColor = Color.white;
                BgStyle.fontSize = 18;
                BgStyle.fontStyle = FontStyle.Normal;
                BgStyle.alignment = TextAnchor.UpperCenter;
            }

            if (CornerStyle == null)
            {
                CornerStyle = new GUIStyle();
                CornerStyle.normal.background = BtnTexture;
                CornerStyle.onNormal.background = BtnTexture;
                CornerStyle.active.background = BtnTexture;
                CornerStyle.onActive.background = BtnTexture;
                CornerStyle.normal.textColor = Color.white;
                CornerStyle.onNormal.textColor = Color.white;
                CornerStyle.active.textColor = Color.white;
                CornerStyle.onActive.textColor = Color.white;
                CornerStyle.fontSize = 18;
                CornerStyle.fontStyle = FontStyle.Normal;
                CornerStyle.alignment = TextAnchor.UpperCenter;
            }

            if (LabelStyle == null)
            {
                LabelStyle = new GUIStyle();
                LabelStyle.normal.textColor = Color.white;
                LabelStyle.onNormal.textColor = Color.white;
                LabelStyle.active.textColor = Color.white;
                LabelStyle.onActive.textColor = Color.white;
                LabelStyle.fontSize = 18;
                LabelStyle.fontStyle = FontStyle.Normal;
                LabelStyle.alignment = TextAnchor.UpperCenter;
            }

            if (OffStyle == null)
            {
                OffStyle = new GUIStyle();
                OffStyle.normal.background = OffTexture;
                OffStyle.onNormal.background = OffTexture;
                OffStyle.active.background = OffPressTexture;
                OffStyle.onActive.background = OffPressTexture;
                OffStyle.normal.textColor = Color.white;
                OffStyle.onNormal.textColor = Color.white;
                OffStyle.active.textColor = Color.white;
                OffStyle.onActive.textColor = Color.white;
                OffStyle.fontSize = 18;
                OffStyle.fontStyle = FontStyle.Normal;
                OffStyle.alignment = TextAnchor.MiddleCenter;
            }

            if (OnStyle == null)
            {
                OnStyle = new GUIStyle();
                OnStyle.normal.background = OnTexture;
                OnStyle.onNormal.background = OnTexture;
                OnStyle.active.background = OnPressTexture;
                OnStyle.onActive.background = OnPressTexture;
                OnStyle.normal.textColor = Color.white;
                OnStyle.onNormal.textColor = Color.white;
                OnStyle.active.textColor = Color.white;
                OnStyle.onActive.textColor = Color.white;
                OnStyle.fontSize = 18;
                OnStyle.fontStyle = FontStyle.Normal;
                OnStyle.alignment = TextAnchor.MiddleCenter;
            }

            if (BtnStyle == null)
            {
                BtnStyle = new GUIStyle();
                BtnStyle.normal.background = BtnTexture;
                BtnStyle.onNormal.background = BtnTexture;
                BtnStyle.active.background = BtnPressTexture;
                BtnStyle.onActive.background = BtnPressTexture;
                BtnStyle.normal.textColor = Color.white;
                BtnStyle.onNormal.textColor = Color.white;
                BtnStyle.active.textColor = Color.white;
                BtnStyle.onActive.textColor = Color.white;
                BtnStyle.fontSize = 18;
                BtnStyle.fontStyle = FontStyle.Normal;
                BtnStyle.alignment = TextAnchor.MiddleCenter;
            }
        }

        public void Update()
        {
            CharacterRoutine();
            CheckInputs();
            StatsRoutine();
        }

        private void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                _isMenuOpen = !_isMenuOpen;
                GetCharacter();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                GiveMoney();
            }
        }

        private void CharacterRoutine()
        {
            if (!_CharacterCollected)
            {
                GetCharacter();
            }
        }

        private void ESPRoutine()
        {
            if (renderInteractables)
            {
                RenderInteractables();
            }
        }

        private void StatsRoutine()
        {
            if (_CharacterCollected)
            {
                if (skillToggle)
                {
                    LocalSkills.ApplyAmmoPack();
                }
            }
        }


        public static void SetBG(int windowID)
        {
            GUI.Box(new Rect(0f, 0f, widthSize + 10, 50f + 45 * mulY), "", CornerStyle);
            if (Event.current.type == EventType.MouseDrag)
            {
                delay += Time.deltaTime;
                if (delay > 0.3f)
                {
                    _ifDragged = true;
                }
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                delay = 0;
                if (!_ifDragged)
                    _isMenuOpen = !_isMenuOpen;
                _ifDragged = false;
            }
            GUI.DragWindow();
        }

        public static void DrawMenu()
        {
            
            GUI.Box(new Rect(rect.x + 0f, rect.y + 0f, widthSize + 10, 50f + 45 * mulY), "", BgStyle);
            GUI.Label(new Rect(rect.x + 0f, rect.y + 5f, widthSize + 10, 95f), "Spektre menu\nv 0.03", LabelStyle);
           

            if (_CharacterCollected)
            {
               
                // we dont have a god toggle bool, because we can just ref localhealth
                if (LocalHealth.godMode)
                {
                    if (GUI.Button(BtnRect(1, false), "God mode: ON", OnStyle))
                    {
                        LocalHealth.godMode = false;
                    }
                }
                else if (GUI.Button(BtnRect(1, false), "God mode: OFF", OffStyle))
                {
                    LocalHealth.godMode = true;
                }

                if (skillToggle)
                {
                    if (GUI.Button(BtnRect(2, false), "Infinite Skills: ON", OnStyle))
                    {
                        skillToggle = false;
                    }
                }
                else if (GUI.Button(BtnRect(2, false), "Infinite Skills: OFF", OffStyle))
                {
                    skillToggle = true;
                }

                if (renderInteractables)
                {
                    if (GUI.Button(BtnRect(3, false), "Interactables ESP: ON", OnStyle))
                    {
                        renderInteractables = false;
                    }
                }
                else if (GUI.Button(BtnRect(3, false), "Interactables ESP: OFF", OffStyle))
                {
                    renderInteractables = true;
                }

                if (GUI.Button(BtnRect(4, true), "Give Money: " + moneyToGive.ToString(), BtnStyle))
                {
                    GiveMoney();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (moneyToGive > 100)
                        moneyToGive -= 100;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (moneyToGive >= 100)
                        moneyToGive += 100;
                }
                if (GUI.Button(BtnRect(5, true), "Give Lunar Coins: " + coinsToGive.ToString(), BtnStyle))
                {
                    GiveLunarCoins();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (coinsToGive > 10)
                        coinsToGive -= 10;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (coinsToGive >= 10)
                        coinsToGive += 10;
                }
                if (GUI.Button(BtnRect(6, true), "Give Experience: " + xpToGive.ToString(), BtnStyle))
                {
                    giveXP();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (xpToGive > 100)
                        xpToGive -= 100;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (xpToGive >= 100)
                        xpToGive += 100;
                }
                if (GUI.Button(BtnRect(7, true), "Roll Items: " + itemsToRoll.ToString(), BtnStyle))
                {
                    RollItems(itemsToRoll.ToString());
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (itemsToRoll > 5)
                        itemsToRoll -= 5;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (itemsToRoll >= 5)
                        itemsToRoll += 5;
                }
                if (GUI.Button(BtnRect(8, true), "Give All Items: " + allItemsQuantity.ToString(), BtnStyle))
                {
                    GiveAllItems();
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 80, rect.y + btnY, 40, 40), "-", OffStyle))
                {
                    if (allItemsQuantity > 1)
                        allItemsQuantity -= 1;
                }
                if (GUI.Button(new Rect(rect.x + widthSize - 35, rect.y + btnY, 40, 40), "+", OffStyle))
                {
                    if (allItemsQuantity >= 1)
                        allItemsQuantity += 1;
                }
                if (GUI.Button(BtnRect(9, true), "Give Item by ID", BtnStyle))
                {
                    int i;
                    i = int.Parse(str);
                    GiveItem(i);
                }
                str = GUI.TextField(new Rect(rect.x + widthSize - 80, rect.y + btnY, 85, 40), str, OffStyle);
                if (GUI.Button(BtnRect(10, false), "Stack Inventory", BtnStyle)) 
                {
                    StackInventory();

                }
                if (GUI.Button(BtnRect(11, false), "Clear Inventory", BtnStyle))
                {

                    ClearInventory();
                }
            }
            else
            {
                GUI.Box(new Rect(rect.x, rect.y + 95f * mulY, widthSize + 10, 50f), "", BgStyle);
                GUI.Label(new Rect(rect.x, rect.y + 95f * mulY, widthSize + 10, 50f), "<color=yellow>Note: Buttons Will Appear in Match Only</color>", LabelStyle);
            }
        }

        // Rect for buttons
        // It automatically auto position buttons. There is no need to change it
        public static Rect BtnRect(int y, bool multiplyBtn)
        {
            mulY = y;
            if (multiplyBtn)
            {
                btnY = 5 + 45 * y;
                return new Rect(rect.x + 5, rect.y + 5 + 45 * y, widthSize - 90, 40);
            }
            return new Rect(rect.x + 5, rect.y + 5 + 45 * y, widthSize, 40);
        }

        // Textures
        public static Texture2D BtnTexture
        {
            get
            {
                if (btntexture == null)
                {
                    btntexture = NewTexture2D;
                    btntexture.SetPixel(0, 0, new Color32(3, 155, 229, 255));
                    btntexture.Apply();
                }
                return btntexture;
            }
        }

        public static Texture2D BtnPressTexture
        {
            get
            {
                if (btnpresstexture == null)
                {
                    btnpresstexture = NewTexture2D;
                    btnpresstexture.SetPixel(0, 0, new Color32(2, 119, 189, 255));
                    btnpresstexture.Apply();
                }
                return btnpresstexture;
            }
        }

        public static Texture2D OnPressTexture
        {
            get
            {
                if (onpresstexture == null)
                {
                    onpresstexture = NewTexture2D;
                    onpresstexture.SetPixel(0, 0, new Color32(62, 119, 64, 255));
                    onpresstexture.Apply();
                }
                return onpresstexture;
            }
        }

        public static Texture2D OnTexture
        {
            get
            {
                if (ontexture == null)
                {
                    ontexture = NewTexture2D;
                    ontexture.SetPixel(0, 0, new Color32(79, 153, 82, 255));
                    ontexture.Apply();
                }
                return ontexture;
            }
        }

        public static Texture2D OffPressTexture
        {
            get
            {
                if (offpresstexture == null)
                {
                    offpresstexture = NewTexture2D;
                    offpresstexture.SetPixel(0, 0, new Color32(79, 79, 79, 255));
                    offpresstexture.Apply();
                }
                return offpresstexture;
            }
        }

        public static Texture2D OffTexture
        {
            get
            {
                if (offtexture == null)
                {
                    offtexture = NewTexture2D;
                    offtexture.SetPixel(0, 0, new Color32(99, 99, 99, 255));
                    offtexture.Apply();
                }
                return offtexture;
            }
        }

        public static Texture2D BackTexture
        {
            get
            {
                if (backtexture == null)
                {
                    backtexture = NewTexture2D;
                    //ToHtmlStringRGBA  new Color(33, 150, 243, 1)
                    backtexture.SetPixel(0, 0, new Color32(42, 42, 42, 200));
                    backtexture.Apply();
                }
                return backtexture;
            }
        }

        public static Texture2D CornerTexture
        {
            get
            {
                if (cornertexture == null)
                {
                    cornertexture = NewTexture2D;
                    //ToHtmlStringRGBA  new Color(33, 150, 243, 1)
                    cornertexture.SetPixel(0, 0, new Color32(42, 42, 42, 0));
                    cornertexture.Apply();
                }
                return cornertexture;
            }
        }

        // random items
        private static void RollItems(string ammount)
        {
            try
            {
                int num;
                TextSerialization.TryParseInvariant(ammount, out num);
                if (num > 0)
                {
                    WeightedSelection<List<PickupIndex>> weightedSelection = new WeightedSelection<List<PickupIndex>>(8);
                    weightedSelection.AddChoice(Run.instance.availableTier1DropList, 80f);
                    weightedSelection.AddChoice(Run.instance.availableTier2DropList, 19f);
                    weightedSelection.AddChoice(Run.instance.availableTier3DropList, 1f);
                    for (int i = 0; i < num; i++)
                    {
                        List<PickupIndex> list = weightedSelection.Evaluate(UnityEngine.Random.value);
                        LocalPlayerInv.GiveItem(list[UnityEngine.Random.Range(0, list.Count)].itemIndex, 1);
                    }
                }
            }
            catch (ArgumentException)
            {
            }
        }

        // try and setup our character, if we hit an error we set it to false
        //TODO: Find a way to stop it from checking whilst in main menu/lobby menu
        private static void GetCharacter()
        {
            try
            {                
                LocalNetworkUser = null;
                foreach (NetworkUser readOnlyInstance in NetworkUser.readOnlyInstancesList)
                {
                    //localplayer == you!
                    if (readOnlyInstance.isLocalPlayer)
                    {                      
                        LocalNetworkUser = readOnlyInstance;
                        LocalPlayer = LocalNetworkUser.master;
                        LocalPlayerInv = LocalPlayer.GetComponent<Inventory>();
                        LocalHealth = LocalPlayer.GetBody().GetComponent<HealthComponent>();
                        LocalSkills = LocalPlayer.GetBody().GetComponent<SkillLocator>();
                        if (LocalPlayer.alive) _CharacterCollected = true;
                        else _CharacterCollected = false;
                    }
                }         
            }
            catch (Exception e)
            {
                _CharacterCollected = false;
            }
        }
        //clears inventory, duh.
        private static void ClearInventory()
        {
            if (LocalPlayerInv)
            {
                for (ItemIndex itemIndex = ItemIndex.Syringe; itemIndex < (ItemIndex)78; itemIndex++)
                {
                    //If an item exists, delete the whole stack of it
                    int itemCount = LocalPlayerInv.GetItemCount(itemIndex);
                    if (itemCount>0)
                    LocalPlayerInv.RemoveItem(itemIndex,itemCount);
                    
                }
                LocalPlayerInv.SetEquipmentIndex(EquipmentIndex.None);
            }
        }
        //TODO:
        //Seems like after giving all items and removing all items, something breaks.
        //throws ArgumentException: Destination array was not long enough. Look into later.
        private static void GiveAllItems()
        {
            if (LocalPlayerInv)
            {
                for (ItemIndex itemIndex = ItemIndex.Syringe; itemIndex < (ItemIndex)78; itemIndex++)
                {
                    //plantonhit kills you when you pick it up
                    if (itemIndex == ItemIndex.PlantOnHit)
                        continue;
                    //ResetItem sets quantity to 1, RemoveItem removes the last one.
                    LocalPlayerInv.GiveItem(itemIndex, allItemsQuantity);
                }
            }
        }


        private static void GiveItem(int itemIndex)
        {
            if (LocalPlayerInv)
                LocalPlayerInv.GiveItem((ItemIndex)itemIndex);
        }

        private static void StackInventory()
        {
            //Does the same thing as the shrine of order. Orders all your items into stacks of several random items.
            LocalPlayerInv.ShrineRestackInventory(Run.instance.runRNG);
        }
        // self explanatory
        private static void giveXP()
        {
            LocalPlayer.GiveExperience(xpToGive);
        }
        private static void GiveMoney()
        {
            LocalPlayer.GiveMoney(moneyToGive);
        }
        //uh, duh.
        private static void GiveLunarCoins()
        {
            LocalNetworkUser.AwardLunarCoins(coinsToGive);
        }

        private static void RenderInteractables()
        {
            foreach (PurchaseInteraction purchaseInteraction in PurchaseInteraction.FindObjectsOfType(typeof(PurchaseInteraction)))
            {
                if(purchaseInteraction.available)
                {
                    float distanceToObject = Vector3.Distance(Camera.main.transform.position, purchaseInteraction.transform.position);
                    Vector3 Position = Camera.main.WorldToScreenPoint(purchaseInteraction.transform.position);
                    var BoundingVector = new Vector3(Position.x, Position.y, Position.z);
                    if (BoundingVector.z > 0.01)
                    {
                        GUI.color = Color.green;
                        int distance = (int)distanceToObject;
                        String friendlyName = purchaseInteraction.GetDisplayName();
                        int cost = purchaseInteraction.cost;
                        string boxText = $"{friendlyName}\n${cost}\n{distance}m";
                        GUI.Label(new Rect(BoundingVector.x - 50f, (float)Screen.height - BoundingVector.y, 100f, 50f), boxText);
                    }
                }
            }
        }
    }
}
