using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using StudioForge.TotalMiner;
namespace TMFMP.TMInternal
{
    public class Hand
    {
        public object _InternalHandObject;
        public object InternalHandObject
        {
            get
            {
                return this._InternalHandObject;
            }

        }
        public Type InternalHandObjectType
        {
            get
            {
                return this._InternalHandObject.GetType();
            }
        }

        public Hand OtherHand
        {
            get
            {
                Type thisType = this.InternalHandObjectType;
                FieldInfo ownerField = thisType.GetField("owner", BindingFlags.NonPublic | BindingFlags.Instance);
                object ownerObject = ownerField.GetValue(this._InternalHandObject);
                if (this.HandType != InventoryHand.Left)
                {
                    FieldInfo leftHandField = ownerObject.GetType().GetField("LeftHand", BindingFlags.Public | BindingFlags.Instance);
                    return Hand.CreateFromInternalHandObject(leftHandField.GetValue(ownerObject));
                }
                FieldInfo rightHandField = ownerObject.GetType().GetField("RightHand", BindingFlags.Public | BindingFlags.Instance);
                return Hand.CreateFromInternalHandObject(rightHandField.GetValue(ownerObject));
            }
        }
        public InventoryHand HandType
        {
            get
            {
                Type thisType = this.InternalHandObjectType;
                FieldInfo handTypeField = thisType.GetField("HandType", BindingFlags.Public | BindingFlags.Instance);
                return (InventoryHand)handTypeField.GetValue(this._InternalHandObject);
            }
            set
            {
                Type thisType = this.InternalHandObjectType;
                FieldInfo handTypeField = thisType.GetField("HandType", BindingFlags.Public | BindingFlags.Instance);
                handTypeField.SetValue(this._InternalHandObject, value);
            }
        }

        public void SetItem(Item item)
        {
            Type thisType = this.InternalHandObjectType;
            MethodInfo setItemMethod = thisType.GetMethod("SetItem", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(Item) }, null);
            setItemMethod.Invoke(this._InternalHandObject, new object[] { item });
        }
        public void ClearSwing()
        {
            Type thisType = this.InternalHandObjectType;
            MethodInfo clearSwingMethod = thisType.GetMethod("ClearSwing", BindingFlags.Public | BindingFlags.Instance);
            clearSwingMethod.Invoke(this._InternalHandObject, null);
        }
        public void SetIsSwinging(bool b)
        {
            Type thisType = this.InternalHandObjectType;
            MethodInfo setIsSwingingMethod = thisType.GetMethod("SetIsSwinging", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(bool) }, null);
            setIsSwingingMethod.Invoke(this._InternalHandObject, new object[] { b });
        }

        public static Hand CreateFromInternalHandObject(object internal_hand_object)
        {
            Hand hand = new Hand();
            hand._InternalHandObject = internal_hand_object;
            return hand;
        }
        //public static Hand CreateFromNPCBase(NpcBase npc, InventoryHand hand)
        //{
        //    Type thisType = TM.Reflection.TMReflection.GetAsmType("StudioForge.TotalMiner.Hand");
        //    Type actorType = TM.Reflection.TMReflection.GetAsmType("StudioForge.TotalMiner.Actor");
        //    ConstructorInfo ctor = thisType.GetConstructor(new Type[] { actorType, typeof(InventoryHand) });
        //    Hand newHand = Hand.CreateFromInternalHandObject(ctor.Invoke(new object[] { npc.NpcBaseObject, hand }));
        //    return newHand;
        //}
    }
}
