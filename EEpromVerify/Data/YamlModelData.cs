using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsMotionControl.Data
{
    public class _SecGemData
    {
        public string OperatorId { get; set; }
        public string RecipeId { get; set; }
        public string MaterialId { get; set; }
        public string MaterialType { get; set; }
        public int ModelNo { get; set; }
        public List<string> Modellist { get; set; }
    }
    public class _MesData
    {
        public string OperatorId { get; set; }
    }
    public class RootModel
    {
        public _SecGemData SecGemData { get; set; }
        public _MesData MesData { get; set; }
    }

    public class UgcSetFile
    {
        public string ugcFilePath { get; set; }

    }

}
