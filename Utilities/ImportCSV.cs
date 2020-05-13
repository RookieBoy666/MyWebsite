using System;
using System.Data;
using System.IO;
using System.Text;

namespace Utilities
{
    public class ImportCSV
    {
        private string path;

        private string fileName;

        public ImportCSV(string filePath, string fileName)//构造函数
        {
            this.path = filePath;
            this.fileName = fileName;
        }

        public DataTable ReadCsvFileToTable()
        {
            return ReadCsvFileToTable(true, ',');
        }

        public DataTable ReadCsvFileToTable(bool HeadYes, char span)
        {
            //文件路径和文件名
            string files = path + fileName;
            DataTable dt = new DataTable();
            StreamReader fileReader = new StreamReader(files, Encoding.Default);
            try
            {
                //是否为第一行（如果HeadYes为TRUE，则第一行为标题行）
                int lsi = 0;
                //列之间的分隔符
                char cv = span;
                while (fileReader.EndOfStream == false)
                {
                    string line = fileReader.ReadLine();
                    string[] y = line.Split(cv);
                    //第一行为标题行
                    if (HeadYes == true)
                    {
                        //第一行
                        if (lsi == 0)
                        {
                            for (int i = 0; i < y.Length; i++)
                            {
                                dt.Columns.Add(y[i].Trim().ToString());
                            }
                            lsi++;
                        }
                        //从第二列开始为数据列
                        else
                        {
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < y.Length; i++)
                            {
                                dr[i] = y[i].Trim();
                            }
                            dt.Rows.Add(dr);
                        }
                    }

                    //第一行不为标题行

                    else
                    {

                        if (lsi == 0)
                        {
                            for (int i = 0; i < y.Length; i++)
                            {
                                dt.Columns.Add(i.ToString());
                            }
                            lsi++;
                        }

                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < y.Length; i++)
                        {
                            dr[i] = y[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                fileReader.Close();
                fileReader.Dispose();
            }
            return dt;

        }

    }
}
