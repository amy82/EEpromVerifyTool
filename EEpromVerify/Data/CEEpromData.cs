using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;


namespace ApsMotionControl.Data
{
    public class CEEpromData
    {
        public DataTable dataTable = new DataTable();


        public List<EEpromCsvData> dataList;
        // = ReadCsvToList(filePath);
        public CEEpromData()
        {

            

            
        }
        public void LoadExcelData()
        {
            string filePath = string.Format(@"{0}\30.csv", Application.StartupPath); //file path
            dataList = ReadCsvToList(filePath);
        }
        public void SaveExcelData()
        {


        }
        public void WriteCsvFromList(string filePath, List<EEpromCsvData> dataList)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",", //  콤마(,) 구분자 적용
                TrimOptions = TrimOptions.Trim // 공백 자동 제거
            }))
            {
                csv.WriteHeader<EEpromCsvData>(); //  헤더 작성
                csv.NextRecord(); //  다음 줄로 이동
                csv.WriteRecords(dataList); //  데이터 작성
            }
        }
        private List<EEpromCsvData> ReadCsvToList(string filePath)
        {
            //(x) 1.SHOPID	        
            //(x) 2.PRODID
            //3.PROCID	
            //4.EEP_ITEM	
            //5.ADDRESS	
            //6.DATA_SIZE	
            //7.DATA_FORMAT	
            //8.BYTE_ORDER	
            //9.FIX_YN	
            //(x) 10.ITEM_CODE
            //11.ITEM_VALUE	
            //12.CRC_START	
            //13.CRC_END	
            //(x) 14.PAD_VALUE	
            //(x) 15.PAD_POSITION

            string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath)); // 임시 파일 생성

            try
            {
                File.Copy(filePath, tempPath, true); // 원본 CSV를 임시 폴더로 복사

                using (var reader = new StreamReader(tempPath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",", // 콤마(,) 구분자 사용        //"\t", // 탭 구분자 사용
                    HasHeaderRecord = true, // 첫 번째 행을 헤더로 인식
                    TrimOptions = TrimOptions.Trim, // 공백 제거
                    IgnoreBlankLines = true // 빈 줄 무시
                }))
                {
                    return new List<EEpromCsvData>(csv.GetRecords<EEpromCsvData>());
                }
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath); // 읽기 완료 후 임시 파일 삭제
            }
            //using (var reader = new StreamReader(filePath))
            //using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    Delimiter = "\t", // 탭 구분자 사용
            //    HasHeaderRecord = true, // 첫 번째 행을 헤더로 인식
            //    TrimOptions = TrimOptions.Trim, // 공백 제거
            //    IgnoreBlankLines = true // 빈 줄 무시
            //}))
            //{
            //    return new List<EepromData>(csv.GetRecords<EepromData>());
            //}
        }
        /*
        public void ReadExcelData(string fileName)
        {
            int i = 0;
            int j = 0;
            Excel.Application application = new Excel.Application();
            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine(application.DefaultFilePath);

            Excel.Workbook workbook = application.Workbooks.Open(Filename: @fileName);


            //Excel.Worksheet worksheet1 = workbook.Worksheets.get_Item("sheet1");    //물류IO
            Excel.Worksheet worksheet1 = (Excel.Worksheet)workbook.Worksheets[1]; // 첫 번째 시트

            application.Visible = false;
            Excel.Range range = worksheet1.UsedRange;

            string[] titleArr = { "PROCID", "EEP_ITEM", "ADDRESS", "DATA_SIZE", "DATA_FORMAT", "BYTE_ORDER", "FIX_YN", "ITEM_VALUE", "CRC_START", "CRC_END" };
            //PROCID	EEP_ITEM	ADDRESS	DATA_SIZE	DATA_FORMAT	BYTE_ORDER	FIX_YN ITEM_VALUE	CRC_START	CRC_END

            //(x) 1.SHOPID	        
            //(x) 2.PRODID
            //3.PROCID	
            //4.EEP_ITEM	
            //5.ADDRESS	
            //6.DATA_SIZE	
            //7.DATA_FORMAT	
            //8.BYTE_ORDER	
            //9.FIX_YN	
            //(x) 10.ITEM_CODE
            //11.ITEM_VALUE	
            //12.CRC_START	
            //13.CRC_END	
            //(x) 14.PAD_VALUE	
            //(x) 15.PAD_POSITION

            for (i = 0; i < titleArr.Length; i++)
            {
                dataTable.Columns.Add(new DataColumn(titleArr[i], typeof(string)));
            }
            DataRow row;
            for (j = 2; j <= range.Rows.Count; ++j)
            {
                String temp = "";
                row = dataTable.NewRow();

                object obj = (range.Cells[j, 4] as Excel.Range).Value2;

                if (obj != null)
                {
                    temp = obj.ToString();
                }
                row[0] = temp;
                //
                temp = "";
                obj = (range.Cells[j, 9] as Excel.Range).Value2;
                if (obj != null)
                {
                    temp = obj.ToString();
                }
                row[1] = temp;
                dataTable.Rows.Add(row);
            }


            DeleteObject(worksheet1);

            workbook.Close(false, Type.Missing, Type.Missing);
            DeleteObject(workbook);
            application.Quit();
            DeleteObject(application);
        }
        */
        private void DeleteObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Debug.WriteLine($"메모리 할당을 해제하는 중 문제가 발생하였습니다." + ex.ToString());
                //MessageBox.Show("메모리 할당을 해제하는 중 문제가 발생하였습니다." + ex.ToString(), "경고!");
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
