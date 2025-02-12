using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsMotionControl.Data
{
    public class DataManageClass
    {
        public TeachingData teachingData = new TeachingData();
        public IoData ioData = new IoData();
        public WorkData workData = new WorkData();
        public TaskWork TaskWork = new TaskWork();

        public CMesData mesData = new CMesData();
        
        //public RootModel MesData { get; private set; }
    }
    public class CPath
    {
        //BASE
        public const string BASE_PATH = "D:\\EVMS\\EEPROM_VERIFY";
        
        //CONFIG
        public const string BASE_DATA_PATH = "D:\\EVMS\\EEPROM_VERIFY\\Data";
        //LAON
        public const string MIU_DIR = "D:\\EVMS\\EEPROM_VERIFY\\Initialize";
        //Mes
        public const string BASE_SECSGEM_PATH = "D:\\EVMS\\EEPROM_VERIFY\\SecsGem";
        public const string BASE_UBISAM_PATH = "D:\\EVMS\\EEPROM_VERIFY\\SecsGem\\ugc";
        public const string BASE_RECIPE_PATH = "D:\\EVMS\\EEPROM_VERIFY\\SecsGem\\Recipe";

        //LOG
        public const string BASE_LOG_PATH = "D:\\EVMS\\LOG";
        public const string BASE_LOG_ALARM_PATH = "D:\\EVMS\\LOG\\ALARM";


        public const string yamlFilePathModel = "SecGemData.yaml";
        public const string yamlFilePathConfig = "equip_config.yaml";
        public const string yamlFilePathImage = "imageData.yaml";
        public const string yamlFilePathUgc = "ugcFilePath.yaml";
        public const string yamlFilePathRecipe = "Recipe.yaml";
        public const string yamlFilePathProduct = "products.yaml";
        public const string yamlFilePathUser = "users.yaml";
        public const string yamlFilePathAlarm = "Alarm.yaml";   //ex) Alarm_20250204  하루씩


        //
    }
}
