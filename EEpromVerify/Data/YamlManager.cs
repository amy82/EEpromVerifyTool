﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


namespace ApsMotionControl.Data
{
    public class YamlManager
    {
        //private readonly string _filePath;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        // 데이터를 보관할 속성
        public RootModel MesData { get; private set; }
        public RootRecipe vPPRecipeSpecEquip { get; set; }
        public RootRecipe vPPRecipeSpec__Host { get; set; }
        public UgcSetFile ugcSetFile { get; private set; }
        public ConfigData configData { get; private set; }

        public List<string> recipeYamlFiles = new List<string>();
        public AlarmData alarmData {get; set;}
        public ImageData imageData { get; set; }

        public _TaskData TaskData { get; private set; }

        public YamlManager()
        {
            // YAML Serializer & Deserializer 설정
            _serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // CamelCase 사용
                .Build();

            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }
        public bool RecipeYamlFileCopy(string copyPPid, string createPPid)
        {
            string folderPath = CPath.BASE_RECIPE_PATH; // 복사할 폴더 경로


            string sourcePath = Path.Combine(folderPath, copyPPid + ".yaml");
            string destinationPath = Path.Combine(folderPath, createPPid + ".yaml");

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath, true); // true = 같은 파일이 있으면 덮어쓰기
                    Console.WriteLine($"복사 완료: {destinationPath}");

                    return true;
                }
                else
                {
                    Console.WriteLine("원본 파일이 존재하지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
            }

            return false;
        }
        public bool RecipeYamlFileDel(string ppid)
        {
            string folderPath = CPath.BASE_RECIPE_PATH; // 검색할 폴더 경로

            string filePath = Path.Combine(folderPath, ppid + ".yaml");
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine($"삭제됨: {filePath}");
                    return true;
                }
                else
                {
                    Console.WriteLine("파일이 존재하지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
            }
            return false;
        }
        public bool RecipeYamlListLoad()
        {
            string folderPath = CPath.BASE_RECIPE_PATH; // 검색할 폴더 경로
            recipeYamlFiles.Clear();

            string[] files = Directory.GetFiles(folderPath, "*.yaml"); // 모든 .yaml 파일 가져오기

            // 확장자가 .yaml인 파일만 가져오기
            //recipeYamlFiles.AddRange(Directory.GetFiles(folderPath, "*.yaml"));
            foreach (string file in files)
            {
                //string fileName = Path.GetFileName(file); // 파일명만 추출 확장자 포함
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file); //확장자 제외
                recipeYamlFiles.Add(fileNameWithoutExt);
            }

            // 결과 출력
            foreach (var file in recipeYamlFiles)
            {
                Console.WriteLine(file);
            }


            return true;
        }
        public RootRecipe RecipeLoad(string recipeFilePPid)
        {
            //string filePath = Path.Combine(CPath.BASE_RECIPE_PATH, CPath.yamlFilePathRecipe);
            string filePath = Path.Combine(CPath.BASE_RECIPE_PATH, recipeFilePPid + ".yaml");
            RootRecipe tempRecipe = null;
            try
            {
                
                if (!File.Exists(filePath))
                    return tempRecipe;

                // vPPRecipeSpecEquip = LoadYaml<RootRecipe>(filePath);
                tempRecipe = LoadYaml<RootRecipe>(filePath);
                return tempRecipe;

                //if (tempRecipe == null)
                //{
                //    return tempRecipe;
                //}
                // PP_RECIPE_SPEC 정보 출력
                //Console.WriteLine($"PPId: {vPPRecipeSpecEquip.RECIPE.Ppid}");
                //Console.WriteLine($"VERSION: {vPPRecipeSpecEquip.RECIPE.Version}");

                //Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task1"] = new Data.Param { value = 999, use = true };
                //Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task 1"] = new Data.Param { value = 888, use = true };

                //Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task2"] = new Data.Param { value = 1, use = Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task1"].use };

                // ParamMap 출력 (Value와 Flag 값 출력)
                //foreach (var kvp in vPPRecipeSpecEquip.RECIPE.ParamMap)
                //{
                //    Console.WriteLine($"Task: {kvp.Key}, Value: {kvp.Value.value}, Flag: {kvp.Value.use}");
                //}
                //return tempRecipe;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading RecipeLoad: {ex.Message}");
                return tempRecipe;
            }
        }
        public bool RecipeSave(RootRecipe ppRecipe)
        {
            string filePath = Path.Combine(CPath.BASE_RECIPE_PATH, ppRecipe.RECIPE.Ppid +".yaml");//   CPath.yamlFilePathRecipe);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                SaveYaml(filePath, ppRecipe);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// YAML 데이터를 불러옵니다.
        /// </summary>
        public bool MesLoad()
        {
            //string filePath = CPath.yamlFilePathModel;
            string filePath = Path.Combine(CPath.BASE_SECSGEM_PATH, CPath.yamlFilePathModel);
            try
            {
                if (!File.Exists(filePath))
                    return false;
                MesData = LoadYaml<RootModel>(filePath);
                if (MesData == null)
                {
                    return false;
                }

                Globalo.dataManage.mesData.m_sMesOperatorID = MesData.SecGemData.OperatorId;
                Globalo.dataManage.mesData.m_sMesPPID = MesData.SecGemData.RecipeId;
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading MesLoad: {ex.Message}");
                return false;
            }
        }

        public bool MesSave()
        {
            //string filePath = CPath.yamlFilePathModel;
            string filePath = Path.Combine(CPath.BASE_SECSGEM_PATH, CPath.yamlFilePathModel);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                SaveYaml(filePath, MesData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }
        }
        public bool configDataSave()
        {
            string filePath = Path.Combine(CPath.BASE_DATA_PATH, CPath.yamlFilePathConfig);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                SaveYaml(filePath, configData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }
        }
        public bool configDataLoad()
        {
            string filePath = Path.Combine(CPath.BASE_DATA_PATH, CPath.yamlFilePathConfig);
            try
            {
                if (!File.Exists(filePath))
                    return false;


                configData = LoadYaml<ConfigData>(filePath);
                if (configData == null)
                {
                    configData = new ConfigData();
                    return false;
                }
                ///configData.ugcFilePath = Path.Combine(CPath.BASE_UBISAM_PATH, configData.ugcFilePath);
                //// 결과 출력
                //Console.WriteLine($"OperatorId: {MesData.SecGemData.OperatorId}");
                //foreach (var model in MesData.SecGemData.Modellist)
                //{
                //    Console.WriteLine($"- {model}");
                //}
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading YAML: {ex.Message}");
                return false;
            }
        }
        public bool imageDataSave()
        {
            string filePath = Path.Combine(CPath.BASE_DATA_PATH, CPath.yamlFilePathImage);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                SaveYaml(filePath, imageData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }
        }
        public bool imageDataLoad()
        {
            string filePath = Path.Combine(CPath.BASE_DATA_PATH, CPath.yamlFilePathImage);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                imageData = LoadYaml<ImageData>(filePath);
                if (imageData == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading YAML: {ex.Message}");
                return false;
            }
        }
        //
        public bool UgcLoad()
        {
            string filePath = Path.Combine(CPath.BASE_UBISAM_PATH, "UIConfig", CPath.yamlFilePathUgc);
            try
            {
                if (!File.Exists(filePath))
                    return false;


                //Path.Combine(CPath.BASE_UBISAM_PATH, "UIConfig", CPath.yamlFilePathUgc)
                ugcSetFile = LoadYaml<UgcSetFile>(filePath);
                if (ugcSetFile == null)
                {
                    return false;
                }
                ugcSetFile.ugcFilePath = Path.Combine(CPath.BASE_UBISAM_PATH, ugcSetFile.ugcFilePath);
                //// 결과 출력
                //Console.WriteLine($"OperatorId: {MesData.SecGemData.OperatorId}");
                //foreach (var model in MesData.SecGemData.Modellist)
                //{
                //    Console.WriteLine($"- {model}");
                //}
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading YAML: {ex.Message}");
                return false;
            }
        }
        public bool TaskDataLoad()
        {
            string filePath = Path.Combine(CPath.BASE_DATA_PATH, CPath.yamlFilePathTask);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                TaskData = LoadYaml<_TaskData>(filePath);
                if (TaskData == null)
                {
                    return false;
                }

                Globalo.dataManage.TaskWork.m_szChipID = TaskData.LotData.BarcodeData;
                Globalo.dataManage.TaskWork.Judge_Total_Count = TaskData.ProductionInfo.TotalCount;
                Globalo.dataManage.TaskWork.Judge_Ok_Count = TaskData.ProductionInfo.OkCount;
                Globalo.dataManage.TaskWork.Judge_Ng_Count = TaskData.ProductionInfo.NgCount;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading TaskDataLoad: {ex.Message}");
                return false;
            }
        }
        public bool TaskDataSave()
        {
            string filePath = Path.Combine(CPath.BASE_DATA_PATH, CPath.yamlFilePathTask);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                SaveYaml(filePath, TaskData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error TaskDataSave: {ex.Message}");
                return false;
            }
        }
        public bool AlarmLoad()
        {
            //Alarm_2025_02_04.yaml

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(CPath.yamlFilePathAlarm); // "Alarm"
            string fileExtension = Path.GetExtension(CPath.yamlFilePathAlarm); // ".yaml"
                                                                               // 현재 날짜를 "yyyy_MM_dd" 형식으로 가져오기
            string currentDate = DateTime.Now.ToString("yyyy_MM_dd");

            string alarmFilePath = $"{fileNameWithoutExtension}_{currentDate}{fileExtension}";

            currentDate = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("D2");
            string filePath = Path.Combine(CPath.BASE_LOG_ALARM_PATH, currentDate, alarmFilePath);

            try
            {
                if (!File.Exists(filePath))
                {
                    alarmData = new AlarmData();
                    return false;
                }
                    

                alarmData = LoadYaml<AlarmData>(filePath);
                if (alarmData == null)
                {
                    alarmData = new AlarmData();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Alarm: {ex.Message}");
                return false;
            }
        }
        public bool AlarmSave()
        {
            //string filePath = Path.Combine(CPath.BASE_LOG_ALARM_PATH, CPath.yamlFilePathAlarm);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(CPath.yamlFilePathAlarm); // "Alarm"
            string fileExtension = Path.GetExtension(CPath.yamlFilePathAlarm); // ".yaml"
                                                                               // 현재 날짜를 "yyyy_MM_dd" 형식으로 가져오기
            string currentDate = DateTime.Now.ToString("yyyy_MM_dd");

            string alarmFilePath = $"{fileNameWithoutExtension}_{currentDate}{fileExtension}";

            currentDate = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("D2");
            string filePath = Path.Combine(CPath.BASE_LOG_ALARM_PATH, currentDate, alarmFilePath);
            try
            {
                //if (!File.Exists(filePath))       //없으면 생성된다.
                //    return false;

                SaveYaml(filePath, alarmData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save Alarm: {ex.Message}");
                return false;
            }
        }
        public static T LoadYaml<T>(string filePath)
        {
            var deserializer = new DeserializerBuilder().Build();
            using (var reader = new StreamReader(filePath))
            {
                return deserializer.Deserialize<T>(reader);
            }
        }
        // 객체를 YAML 형식으로 저장하는 메서드
        public static void SaveYaml(string filePath, object data)
        {
            var serializer = new SerializerBuilder().Build();
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }

            Console.WriteLine($"YAML 파일이 {filePath}에 저장되었습니다.");
        }
    }
}
