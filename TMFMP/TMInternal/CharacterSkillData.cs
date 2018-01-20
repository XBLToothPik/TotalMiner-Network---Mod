using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TMFMP.TMInternal
{
    public class CharacterSkillData
    {
        private object _BaseCharacterSkillDataObject;
        public object BaseCharacterSkillDataObject
        {
            get
            {
                return this._BaseCharacterSkillDataObject;
            }
        }
        public Type BaseCharacterSkillDataType
        {
            get
            {
                return this._BaseCharacterSkillDataObject.GetType();
            }
        }

        public static CharacterSkillData CreateNew()
        {
            CharacterSkillData toRet = new CharacterSkillData();
            Type internalCharacterSkillDataType = TM.Reflection.TMReflection.GetAsmType("StudioForge.TotalMiner.CharacterSkillsData");
            ConstructorInfo ctor = internalCharacterSkillDataType.GetConstructor(Type.EmptyTypes);
            toRet._BaseCharacterSkillDataObject = ctor.Invoke(null);
            return toRet;
        }
        //public static CharacterSkillData GetFromPlayer(Player p)
        //{
        //    CharacterSkillData toRet = new CharacterSkillData();
        //    Type playerType = p.InternalPlayerObjectType;
        //    Type actor2Type = playerType.BaseType;
        //    Type actorType = actor2Type.BaseType;
        //    FieldInfo skillDataField = actorType.GetField("SkillsData", BindingFlags.Public | BindingFlags.Instance);
        //    toRet._BaseCharacterSkillDataObject = skillDataField.GetValue(p.InternalPlayerObject);
        //    return toRet;
        //}
        //public static void SetToPlayer(Player p, CharacterSkillData data)
        //{
        //    Type playerType = p.InternalPlayerObjectType;
        //    FieldInfo skillDataField = playerType.GetField("SkillsData", BindingFlags.Public | BindingFlags.Instance);
        //    skillDataField.SetValue(p.InternalPlayerObject, data._BaseCharacterSkillDataObject);
        //}
    }
}
