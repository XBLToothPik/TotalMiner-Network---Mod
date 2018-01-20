using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Reflection;
using StudioForge.TotalMiner;
using StudioForge.BlockWorld;
namespace TMFMP.TMInternal
{
    public class NpcBase
    {
        private object _NpcBaseObject;
        public object NpcBaseObject
        {
            get
            {
                return this._NpcBaseObject;
            }
        }
        public Type NpcBaseObjectType
        {
            get
            {
                return _NpcBaseObject.GetType();
            }
        }

        public Vector3 Position
        {
            get
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                return (Vector3)(npcObjActorType.GetField("Position", System.Reflection.BindingFlags.Public | BindingFlags.Instance).GetValue(_NpcBaseObject));
            }
            set
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                npcObjActorType.GetField("Position", System.Reflection.BindingFlags.Public | BindingFlags.Instance).SetValue(_NpcBaseObject, value);
            }
        }
        public Vector3 Velocity
        {
            get
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                return (Vector3)(npcObjActorType.GetField("Velocity", System.Reflection.BindingFlags.Public | BindingFlags.Instance).GetValue(_NpcBaseObject));
            }
            set
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                npcObjActorType.GetField("Velocity", System.Reflection.BindingFlags.Public | BindingFlags.Instance).SetValue(_NpcBaseObject, value);
            }
        }
        public Vector3 ViewDirection
        {
            get
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                return (Vector3)(npcObjActorType.GetField("ViewDirection", System.Reflection.BindingFlags.Public | BindingFlags.Instance).GetValue(_NpcBaseObject));
            }
            set
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                npcObjActorType.GetField("ViewDirection", System.Reflection.BindingFlags.Public | BindingFlags.Instance).SetValue(_NpcBaseObject, value);
            }
        }
        public Vector2 Size
        {
            get
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                return (Vector2)(npcObjActorType.GetField("Size", System.Reflection.BindingFlags.Public | BindingFlags.Instance).GetValue(_NpcBaseObject));
            }
            set
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                npcObjActorType.GetField("Size", System.Reflection.BindingFlags.Public | BindingFlags.Instance).SetValue(_NpcBaseObject, value);
            }
        }

        public Matrix ViewMatrix
        {
            get
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                return (Matrix)(npcObjActorType.GetField("ViewMatrix", System.Reflection.BindingFlags.Public | BindingFlags.Instance).GetValue(_NpcBaseObject));
            
            }
            set
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                npcObjActorType.GetField("ViewMatrix", System.Reflection.BindingFlags.Public | BindingFlags.Instance).SetValue(_NpcBaseObject, value);
            }
        }
        public Matrix ViewMatrixLocal
        {
            get
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                return (Matrix)(npcObjActorType.GetField("ViewMatrixLocal", System.Reflection.BindingFlags.Public | BindingFlags.Instance).GetValue(_NpcBaseObject));
            
            
            }
            set
            {
                Type npcObjType = NpcBaseObjectType;
                Type npcObjActor2Type = npcObjType.BaseType;
                Type npcObjActorType = npcObjActor2Type.BaseType;
                npcObjActorType.GetField("ViewMatrixLocal", System.Reflection.BindingFlags.Public | BindingFlags.Instance).SetValue(_NpcBaseObject, value);
            
            }
        }
        public Matrix ProjectionMatrix
        {
            get
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                return (Matrix)(actorType.GetField("ProjectionMatrix", System.Reflection.BindingFlags.Public | BindingFlags.Instance).GetValue(_NpcBaseObject));


            }
            set
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                actorType.GetField("ProjectionMatrix", System.Reflection.BindingFlags.Public | BindingFlags.Instance).SetValue(_NpcBaseObject, value);
            }
        }

        public Hand LeftHand
        {
            get
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo leftHandField = actorType.GetField("LeftHand", BindingFlags.Public | BindingFlags.Instance);
                return Hand.CreateFromInternalHandObject(leftHandField.GetValue(this._NpcBaseObject));
            }
            set
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo leftHandField = actorType.GetField("LeftHand", BindingFlags.Public | BindingFlags.Instance);
                leftHandField.SetValue(this._NpcBaseObject, value._InternalHandObject);
            }
        }
        public Hand RightHand
        {
            get
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo rightHandField = actorType.GetField("RightHand", BindingFlags.Public | BindingFlags.Instance);
                return Hand.CreateFromInternalHandObject(rightHandField.GetValue(this._NpcBaseObject));
            }
            set
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo rightHandField = actorType.GetField("RightHand", BindingFlags.Public | BindingFlags.Instance);
                rightHandField.SetValue(this._NpcBaseObject, value._InternalHandObject);
            }
        }

        public FlyMode FlyMode
        {
            get
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo flyModeField = actorType.GetField("FlyMode", BindingFlags.Public | BindingFlags.Instance);
                return (FlyMode)flyModeField.GetValue(this._NpcBaseObject);


            }
            set
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo flyModeField = actorType.GetField("FlyMode", BindingFlags.Public | BindingFlags.Instance);
                flyModeField.SetValue(this._NpcBaseObject, value);
            }
        }

        public float Health
        {
            get
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo healthField = actorType.GetField("Health", BindingFlags.Public | BindingFlags.Instance);
                return (float)healthField.GetValue(this._NpcBaseObject);
            }
            set
            {
                Type actorType = TM.Reflection.TMReflection.GetTypeWithLevel(this.NpcBaseObjectType, 2);
                FieldInfo healthField = actorType.GetField("Health", BindingFlags.Public | BindingFlags.Instance);
                healthField.SetValue(this._NpcBaseObject, value);
            }
        }

        public void UpdateRemote()
        {
            Type npcObjType = NpcBaseObjectType;
            MethodInfo updateRemoteMethod = npcObjType.GetMethod("UpdateRemote", BindingFlags.NonPublic | BindingFlags.Instance);
            updateRemoteMethod.Invoke(_NpcBaseObject, null);
        }
        public GlobalPoint3D GetPlaceTarget(Hand hand)
        {
            Type thisType = this.NpcBaseObjectType;
            Type actor2Type = thisType.BaseType;
            Type actorType = actor2Type.BaseType;
            MethodInfo getPlaceTargetMethod = actorType.GetMethod("GetPlaceTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { TM.Reflection.TMReflection.GetAsmType("StudioForge.TotalMiner.Hand") }, null);
            return (GlobalPoint3D)getPlaceTargetMethod.Invoke(this._NpcBaseObject, new object[] { hand.InternalHandObject });
        }

        public Hand GetHand(InventoryHand handType)
        {
            switch (handType)
            {
                case InventoryHand.Left:
                    return this.LeftHand;
                case InventoryHand.Right:
                    return this.RightHand;
                default:
                    throw new Exception("InvalidHandType_GetHand");
            }
        }
        public void CreateHands(InventoryHand hands)
        {
            if ((hands & InventoryHand.Left) == InventoryHand.Left)
            {
                this.LeftHand = Hand.CreateFromNPCBase(this, InventoryHand.Left);
            }
            if ((hands & InventoryHand.Right) == InventoryHand.Right)
            {
                this.RightHand = Hand.CreateFromNPCBase(this, InventoryHand.Right);
            }

        }
        public static NpcBase GetFromAPIObject(object internal_npcBase)
        {
            NpcBase newBase = new NpcBase();
            newBase._NpcBaseObject = internal_npcBase;
            return newBase;
        }

    }
}
