using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TMFMP.TMInternal
{
    public class VoxelModelManager
    {
        private object _BaseVoxelModelManagerObject;
        public object BaseVoxelModelManagerObject
        {
            get
            {
                return this._BaseVoxelModelManagerObject;
            }
        }
        public Type BaseVoxelModelManagerType
        {
            get
            {
                return this._BaseVoxelModelManagerObject.GetType();
            }
        }

        public MapModel LoadModComponent(string comName, bool buildMesh)
        {
            Type thisType = this.BaseVoxelModelManagerType;
            MethodInfo loadModComponentMethod = thisType.GetMethod("LoadModComponent", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(bool) }, null);
            return MapModel.CreateFromInternalObject(loadModComponentMethod.Invoke(this._BaseVoxelModelManagerObject, new object[] { comName, buildMesh }));
        }
        public void UnloadMapComponent(MapModel model)
        {
            Type thisType = this.BaseVoxelModelManagerType;
            Type mapModelComponent = TM.Reflection.TMReflection.GetAsmType("StudioForge.TotalMiner.Graphics.MapModel");
            MethodInfo unloadComponentMethod = thisType.GetMethod("UnloadComponent", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { mapModelComponent }, null);
            unloadComponentMethod.Invoke(this._BaseVoxelModelManagerObject, new object[] { model.BaseMapModelObject });
        }
        public static VoxelModelManager CreateFromGameInstance(GameInstance inst)
        {
            VoxelModelManager toRet = new VoxelModelManager();
            Type gameInstType = inst.BaseGameObjectType;
            FieldInfo voxelModelManagerField = gameInstType.GetField("VoxelModelManager", BindingFlags.Public | BindingFlags.Instance);
            toRet._BaseVoxelModelManagerObject = voxelModelManagerField.GetValue(inst.BaseGameInstancebject);
            return toRet;
        }

     
    }
}
