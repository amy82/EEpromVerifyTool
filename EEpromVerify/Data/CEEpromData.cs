﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static readonly string HEX = "HEX";
        public static readonly string ASCII = "ASCII";
        public static readonly string DEC = "DEC";
        public static readonly string FLOAT = "FLOAT";
        public static readonly string DOUBLE = "DOUBLE";
        public static readonly string UNIX_TIME = "UNIX_TIME";

        public static readonly string CRC_CRC8_DEFAULT = "CRC_CRC8_DEFAULT";
        public static readonly string CRC_CRC8_SAE_J1850 = "CRC_CRC8_SAE_J1850";
        public static readonly string CRC_CRC8_SAE_J1850_ZERO = "CRC_CRC8_SAE_J1850_ZERO";

        public static readonly string CRC_CRC16_CCIT_ZERO = "CRC_CRC16_CCIT_ZERO";
        public static readonly string CRC_CRC16_CCIT_FALSE = "CRC_CRC16_CCIT_FALSE";
        public static readonly string CRC_CHECKSUM16_RFC1071 = "CRC_CHECKSUM16_RFC1071";


        public static readonly string CRC_CHECKSUM_RFC1071 = "CRC_CHECKSUM_RFC1071";


        public static readonly string EMPTY = "EMPTY";


        //public DataTable dataTable = new DataTable();


        public List<MesEEpromCsvData> MesDataList;
        public List<EEpromReadData> EEpromDataList;

        public List<byte> EquipEEpromReadData;
        public CEEpromData()
        {
            checksumTest();
            EndianTest();

            EEpromDataList = new List<EEpromReadData>();
            EquipEEpromReadData = new List<byte>();          //제품에서 읽은 eeprom 값
        }
        public void LoadExcelData()
        {
            string filePath = string.Format(@"{0}\30.csv", Application.StartupPath); //file path
            ReadCsvToList(filePath);
        }
        public void SaveExcelData()
        {

            string filePath = string.Format(@"{0}\30.csv", Application.StartupPath); //file path
            WriteCsvFromList(filePath, MesDataList);
        }

        public static bool EEpromVerifyRun()
        {

            return true;
        }
        public static unsafe bool EEpromDataRead()
        {
            int i = 0;
            bool bRtn = true;
            string slaveAddr = Regex.Replace("0x50", @"\D", "");
            string readAddr = Regex.Replace("0x00", @"\D", "");

            ushort readDataLength = 100;// Convert.ToUInt16(Globalo.mCCdPanel.textBox_ReadDataLeng.Text);  //읽어야될 길이
            //readDataLength = MES에서 받은 데이터에서 확인

            if (readDataLength < 1)
            {
                return false;
            }

            ushort maxReadLength = CLaonGrabberClass.MAX_READ_WRITE_LENGTH;
            if (maxReadLength > readDataLength)
            {
                maxReadLength = readDataLength;
            }

            int errorCode = 0;
            int endAddress = readDataLength;//// 0xE0;  //       241
            //0x513;     //1299

            ushort SlaveAddr = Convert.ToUInt16(slaveAddr, 16); // 0x50;
            ushort StartAddr = Convert.ToUInt16(readAddr, 16); //0x00;

            //ushort checkAddr = 0x3C06;

            byte[] EEpromReadData = new byte[endAddress]; // EEPROM 데이터 읽기


            Globalo.dataManage.eepromData.EquipEEpromReadData.Clear();

            for (i = 0; i < endAddress; i += maxReadLength)     // 0;  i < 129;  i += 30; 
            {
                fixed (byte* pData = EEpromReadData)
                {
                    if ((i + maxReadLength) > endAddress)
                    {
                        //if( ( 0 + 30 ) > 129
                        //if( ( 30 + 30 ) > 129
                        //if( ( 60 + 30 ) > 129
                        //if( ( 90 + 30 ) > 129
                        //if( ( 120 + 30 ) > 129
                        //150

                        maxReadLength = (ushort)((endAddress - i) + 0);    //120 ~ 129 는 10개라서 + 1
                    }
                    errorCode = Globalo.GrabberDll.mReadI2CBurst(SlaveAddr, (ushort)(StartAddr + i), 2, pData + i, (ushort)maxReadLength);
                    if (errorCode != 0)
                    {
                        bRtn = false;
                        Console.WriteLine("mReadI2CBurst errorCode");
                        break;
                    }
                }
            }


            Globalo.dataManage.eepromData.EquipEEpromReadData.AddRange(EEpromReadData);


            return bRtn;
        }
        public bool BinDumpFileSave()
        {


            return true;
        }
        private void WriteCsvFromList(string filePath, List<MesEEpromCsvData> dataList)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",", //  콤마(,) 구분자 적용
                TrimOptions = TrimOptions.Trim // 공백 자동 제거
            }))
            {
                csv.WriteHeader<MesEEpromCsvData>(); //  헤더 작성
                csv.NextRecord(); //  다음 줄로 이동
                csv.WriteRecords(dataList); //  데이터 작성
            }
        }
        //private List<EEpromCsvData> ReadCsvToList(string filePath)
        private bool ReadCsvToList(string filePath)
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
                if (File.Exists(filePath))
                {
                    File.Copy(filePath, tempPath, true); // 원본 CSV를 임시 폴더로 복사
                }
                else
                {
                    return false;
                }
                    

                using (var reader = new StreamReader(tempPath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",", // 콤마(,) 구분자 사용        //"\t", // 탭 구분자 사용
                    HasHeaderRecord = true, // 첫 번째 행을 헤더로 인식
                    TrimOptions = TrimOptions.Trim, // 공백 제거
                    IgnoreBlankLines = true // 빈 줄 무시
                }))
                {
                    MesDataList = new List<MesEEpromCsvData>(csv.GetRecords<MesEEpromCsvData>());
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ReadCsvToList: {ex.Message}");
                return false;
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath); // 읽기 완료 후 임시 파일 삭제
            }
            return true;
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

        public void EndianTest()
        {
            // TODO: FIX-YN 값이 N일 경우에 항상 HEX로 전달되며, UI 표기시에 BYTE_ORDER / DATA_FORMAT 참고하여 변환 필요
            //0x8FA9BBB20BBB7B40 
            //0x8F A9 BB B2 0B BB 7B 40 
            byte[] data4 = { 0x8F, 0xA9, 0xBB, 0xB2, 0x0B, 0xBB, 0x7B, 0x40 };
            if (BitConverter.IsLittleEndian)
            {
                Console.WriteLine($"{BitConverter.IsLittleEndian}");
            }
            double bigEndianDouble = BitConverter.ToDouble(data4, 0);
            if (BitConverter.IsLittleEndian)
            {
                Console.WriteLine($"{BitConverter.IsLittleEndian}");
            }
            Array.Reverse(data4);
            if (BitConverter.IsLittleEndian)
            {
                Console.WriteLine($"{BitConverter.IsLittleEndian}");
            }
            bigEndianDouble = BitConverter.ToDouble(data4, 0);
            if (BitConverter.IsLittleEndian)
            {
                Console.WriteLine($"{BitConverter.IsLittleEndian}");
            }
            List<byte> datalist4 = new List<byte>();
            for (int i = 0; i < data4.Length; i++)
            {
                datalist4.Add(data4[i]);
            }

            string doubleStr = BitConverter.ToString(datalist4.GetRange(0, 7).ToArray().Reverse().ToArray()).Replace("-", "");

            doubleStr = BitConverter.ToSingle(datalist4.GetRange(0, 7).ToArray(), 0).ToString();
            doubleStr = BitConverter.ToSingle(datalist4.GetRange(0, 7).ToArray().Reverse().ToArray(), 0).ToString();
            double doubleValue1 = BitConverter.ToDouble(datalist4.ToArray(), 0);
            if (BitConverter.IsLittleEndian)
            {
                Console.WriteLine($"{BitConverter.IsLittleEndian}");
            }
            //Endian 변환 후 0x407BBB0BB2BBA98F Double 로 변환하면 443.690356 값이 됩니다.
            //EPROM_READ_VALUE = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray().Reverse().ToArray()).Replace("-", "");


            byte[] data__4 = { 0x40, 0x7B, 0xBB, 0x0B, 0xB2, 0xBB, 0xA9, 0x8F };
            datalist4.Clear();
            for (int i = 0; i < data4.Length; i++)
            {
                datalist4.Add(data__4[i]);
            }
            doubleValue1 = BitConverter.ToDouble(datalist4.ToArray(), 0);
            if (BitConverter.IsLittleEndian)
            {
                Console.WriteLine($"{BitConverter.IsLittleEndian}");
            }
        }

        public void checksumTest()
        {
            byte[] data = { 0x01, 0x02, 0x03, 0x04 };
            byte crc8_default = ComputeCRC8(data, 0x07, 0x00, 0x00);  // CRC8_DEFAULT
            byte crc8_sae_j1850 = ComputeCRC8(data, 0x1D, 0xFF, 0xFF); // CRC8_SAE_J1850
            byte crc8_sae_j1850_zero = ComputeCRC8(data, 0x1D, 0x00, 0x00); // CRC8_SAE_J1850_ZERO

            Console.WriteLine($"CRC8_DEFAULT: {crc8_default:X2}");
            Console.WriteLine($"CRC8_SAE_J1850: {crc8_sae_j1850:X2}");
            Console.WriteLine($"CRC8_SAE_J1850_ZERO: {crc8_sae_j1850_zero:X2}");

            byte[] fordData = { 0x01, 0x00, 0x67, 0xAD, 0x57, 0xE9, 0xFF, 0xFF, 0xFF, 0xFF };
            byte fordcrc8_sae_j1850_zero = ComputeCRC8(fordData, 0x1D, 0x00, 0x00); // CRC8_SAE_J1850_ZERO
            Console.WriteLine($"FORD CRC8_SAE_J1850_ZERO: {fordcrc8_sae_j1850_zero:X2}");
            //Ford 결과는 0xE2


            byte[] data2 = { 0x01, 0x02, 0x03, 0x04 };
            ushort crc16_ccitt_zero = ComputeCRC16(data2, 0x1021, 0x0000, 0x0000); // CRC16_CCIT_ZERO
            ushort crc16_ccitt_false = ComputeCRC16(data2, 0x1021, 0xFFFF, 0x0000); // CRC16_CCIT_FALSE

            Console.WriteLine($"CRC16_CCIT_ZERO: {crc16_ccitt_zero:X4}");
            Console.WriteLine($"CRC16_CCIT_FALSE: {crc16_ccitt_false:X4}");



            byte[] data3 = { 0x01, 0x02, 0x03, 0x04 };
            ushort checksum16 = ComputeChecksum16(data3);
            Console.WriteLine($"CHECKSUM16_RFC1071: {checksum16:X4}");


            byte[] testData = Enumerable.Repeat((byte)0x20, 30).ToArray(); // 30개 0x20 값
            ushort checksum = ComputeRFC1071Checksum(testData);

            Console.WriteLine($"Checksum: {checksum:X4}"); // 16진수 출력


            byte[] testData2 = {
            0xAB, 0x9C, 0xFF, 0x89, 0x29, 0x2A, 0xAE, 0x40,
            0x9E, 0xA6, 0x83, 0x65, 0x9E, 0x28, 0xAE, 0x40,
            0x3C, 0xAD, 0x1E, 0xC7, 0x7A, 0x25, 0x9E, 0x40,
            0x76, 0x18, 0xB8, 0xF8, 0x5A, 0x15, 0x91, 0x40,
            0x5D, 0x8C, 0x61, 0x8A, 0xD4, 0xDA, 0xD1, 0xBF,
            0x41, 0x15, 0x8C, 0x2C, 0xB6, 0x22, 0xDF, 0x3F,
            0x48, 0x78, 0x1E, 0x2B, 0x51, 0x34, 0xEA, 0xBF,
            0x89, 0x70, 0x2C, 0x64, 0x57, 0xB1, 0xDD, 0x3F,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0xA3, 0x48, 0xB1, 0x45, 0xCD, 0xCC, 0x04, 0x40
        };

            ushort checksum2 = ComputeRFC1071Checksum(testData2);
            Console.WriteLine($"Checksum: {checksum2:X4}"); // 16진수 출력

            byte[] bytes = BitConverter.GetBytes(checksum2);
            Array.Reverse(bytes);
            ushort littleEndianValue = BitConverter.ToUInt16(bytes, 0);


            
        }

        public static string CrcCommonCalculation(string Type, string order, byte[] data) //, byte polynomial = 0x00, byte initialValue = 0x00, byte xorOut = 0x00)
        {
            string RtnString = "";
            if (Type == CRC_CRC8_SAE_J1850)
            {
                byte crc8_sae_j1850 = ComputeCRC8(data, 0x1D, 0xFF, 0xFF); // CRC8_SAE_J1850
                RtnString = crc8_sae_j1850.ToString("X2");
            }
            else if (Type == CRC_CRC8_SAE_J1850)
            {
                byte crc8_sae_j1850 = ComputeCRC8(data, 0x1D, 0xFF, 0xFF); // CRC8_SAE_J1850
                RtnString = crc8_sae_j1850.ToString("X2");
            }
            else if (Type == CRC_CRC8_SAE_J1850_ZERO)
            {
                byte crc8_sae_j1850_zero = ComputeCRC8(data, 0x1D, 0x00, 0x00); // CRC8_SAE_J1850_ZERO
                RtnString = crc8_sae_j1850_zero.ToString("X2");
            }
            else if (Type == CRC_CRC16_CCIT_ZERO)
            {
                ushort crc16_ccitt_zero = ComputeCRC16(data, 0x1021, 0x0000, 0x0000); // CRC16_CCIT_ZERO
                if (order == "Little")
                {
                    byte[] bytes = BitConverter.GetBytes(crc16_ccitt_zero);
                    Array.Reverse(bytes);
                    ushort littleEndianValue = BitConverter.ToUInt16(bytes, 0);     //Little Endian 뒤집기

                    crc16_ccitt_zero = littleEndianValue;
                }

                RtnString = crc16_ccitt_zero.ToString("X4");
            }
            else if (Type == CRC_CRC16_CCIT_FALSE)
            {
                ushort crc16_ccitt_false = ComputeCRC16(data, 0x1021, 0xFFFF, 0x0000); // CRC16_CCIT_FALSE
                if (order == "Little")
                {
                    byte[] bytes = BitConverter.GetBytes(crc16_ccitt_false);
                    Array.Reverse(bytes);
                    ushort littleEndianValue = BitConverter.ToUInt16(bytes, 0);     //Little Endian 뒤집기

                    crc16_ccitt_false = littleEndianValue;
                }
                RtnString = crc16_ccitt_false.ToString("X4");
            }
            else if (Type == CRC_CHECKSUM16_RFC1071)
            {
                ushort checksum16 = ComputeChecksum16(data);
                if (order == "Little")
                {
                    byte[] bytes = BitConverter.GetBytes(checksum16);
                    Array.Reverse(bytes);
                    ushort littleEndianValue = BitConverter.ToUInt16(bytes, 0);     //Little Endian 뒤집기

                    checksum16 = littleEndianValue;
                }
                RtnString = checksum16.ToString("X4");
            }
            else if (Type == CRC_CHECKSUM_RFC1071)
            {
                ushort checksum = ComputeRFC1071Checksum(data);
                if(order == "Little")
                {
                    byte[] bytes = BitConverter.GetBytes(checksum);
                    Array.Reverse(bytes);
                    ushort littleEndianValue = BitConverter.ToUInt16(bytes, 0);     //Little Endian 뒤집기

                    checksum = littleEndianValue;
                }

                RtnString = checksum.ToString("X4");
            }
            else 
            {
                byte crc8_default = ComputeCRC8(data, 0x07, 0x00, 0x00);  // CRC8_DEFAULT
                RtnString = crc8_default.ToString("X2");
            }
            return RtnString;
        }
        public static string StringToHex(string Input, string Format, string Order, string FixYn)       //MES 에서 받은 값을 변환
        {
            string RtnString = "";
            int i = 0;
            
            if (Format == ASCII && FixYn == "Y")
            {
                StringBuilder hex = new StringBuilder();
                byte[] bytes = Encoding.ASCII.GetBytes(Input); // 문자열 → 바이트 배열 변환

                //위에서 N일 경우가 있어서 무조건
                //Y만 들어온다.
                if(Order == "Little")
                {
                    Array.Reverse(bytes);

                }

                for (i = 0; i < bytes.Length; i++) // 뒤에서부터 추가
                {
                    hex.AppendFormat("{0:X2}", bytes[i]);      //Little Endian 변환 코드
                }

                //foreach (char c in Input)
                //{
                //    hex.AppendFormat("{0:X2} ", (byte)c); // 각 문자를 16진수 2자리로 변환
                //}
                RtnString = hex.ToString().Trim();
            }
            else if (Format == FLOAT && FixYn == "Y")
            {
                float floatValue = float.Parse(Input);
                byte[] bytes = BitConverter.GetBytes(floatValue); // float → byte[]
                if (Order == "Little")
                {
                    Array.Reverse(bytes); // 빅엔디안으로 변환 (네트워크 전송 시 필요)
                }
                
                RtnString = BitConverter.ToString(bytes).Replace("-", "");
            }
            else if (Format == DOUBLE && FixYn == "Y")
            {
                double doubleValue = double.Parse(Input);
                byte[] bytes = BitConverter.GetBytes(doubleValue); // double → byte[]
                if(Order == "Little")
                {
                    Array.Reverse(bytes); // 빅엔디안 변환
                }
                
                RtnString = BitConverter.ToString(bytes).Replace("-", "");
            }
            else// (Format == HEX || Format == EMPTY || Format ==  || FixYn == "N")      //N이면 무조건 Hex로 들어온다.
            {
                Input = Input.Replace("0x", "");
                if (FixYn == "Y" && Order == "Little")
                {
                    //뒤집어야된다.
                    // 2자리씩 나누고 역순으로 정렬

                    //char[] charArray = Input.ToCharArray();
                    //Array.Reverse(charArray);
                    //RtnString = new string(charArray);

                    // 2자리씩 나누기
                    string[] bytes = Enumerable.Range(0, Input.Length / 2)
                                               .Select(j => Input.Substring(j * 2, 2))
                                               .ToArray();

                    // Little Endian 변환 (뒤집기)
                    RtnString = string.Join("", bytes.Reverse());

                }
                else
                {
                    RtnString = Input;
                }

            }




            //MES_EEPROM_VALUE = BitConverter.ToString(Globalo.mCCdPanel.CcdEEpromReadData.GetRange(startAddress, readCount).ToArray()).Replace("-", " ");
            return RtnString; // 마지막 공백 제거
        }



        //CRC-8 계산
        public static byte ComputeCRC8(byte[] data, byte polynomial, byte initialValue, byte xorOut)
        {
            byte crc = initialValue;
            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    crc = (byte)((crc & 0x80) != 0 ? (crc << 1) ^ polynomial : (crc << 1));
                }
            }
            return (byte)(crc ^ xorOut);
        }

        //CRF-16 계산 (CCITT)
        public static ushort ComputeCRC16(byte[] data, ushort polynomial, ushort initialValue, ushort xorOut)
        {
            ushort crc = initialValue;
            foreach (byte b in data)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    crc = (ushort)((crc & 0x8000) != 0 ? (crc << 1) ^ polynomial : (crc << 1));
                }
            }
            return (ushort)(crc ^ xorOut);
        }

        //CHECKSUM16 (RFC1071)
        public static ushort ComputeChecksum16(byte[] data)
        {
            uint sum = 0;
            for (int i = 0; i < data.Length; i += 2)
            {
                ushort word = (ushort)((data[i] << 8) + (i + 1 < data.Length ? data[i + 1] : 0));
                sum += word;
                while ((sum >> 16) > 0)  // Carry 발생 시 추가 처리
                {
                    sum = (sum & 0xFFFF) + (sum >> 16);
                }
            }
            return (ushort)~sum; // 1의 보수 적용
        }


        public static ushort ComputeRFC1071Checksum(byte[] data)
        {
            uint sum = 0;

            // 16비트 단위로 더하기 (2바이트씩 묶어서 처리)
            for (int i = 0; i < data.Length; i += 2)
            {
                ushort word = (ushort)(data[i] << 8); // 상위 바이트
                if (i + 1 < data.Length)
                {
                    word |= data[i + 1]; // 하위 바이트 추가
                }
                sum += word;
            }

            // 16비트 초과한 값을 처리 (Carry Bit Handling)
            while ((sum >> 16) > 0)
            {
                sum = (sum & 0xFFFF) + (sum >> 16);
            }

            // One's Complement 취하기
            return (ushort)~sum;
        }
    }
}
