using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utilities
{
    public class FileLogHelper
    {
        private string _fileNameFormat = null;
        private bool _bLog = true;
        public string Path { get; set; }

        /// <summary>
        /// 设置文件名格式,如果包含时间格式使用<>,比如<yyyy>代表四位的年,<yyyy-MM-dd>则为2007-08-01,具体参考DataTime.ToString()方法的格式
        /// 如果没有指定盘符则使用当前进程所在文件夹
        /// </summary>
        public FileLogHelper(string sFileNameFormat)
        {
            _fileNameFormat = sFileNameFormat;
        }

        public FileLogHelper(string sFileNameFormat, string path)
        {
            _fileNameFormat = sFileNameFormat;
            Path = path;
        }

        public string FileNameFormat
        {
            get { return _fileNameFormat; }
            set { _fileNameFormat = value; }
        }

        public bool Log
        {
            get { return _bLog; }
            set { _bLog = value; }
        }

        public void Write(string strLog)
        {
            Write(false, false, strLog);
        }

        public void WriteWithTime(string strLog)
        {
            Write(true, false, strLog);
        }

        public void WriteLine()
        {
            WriteFile("\r\n");
        }

        public void WriteLine(string strLog)
        {
            Write(false, true, strLog);
        }

        public void WriteLineWithTime(string strLog)
        {
            Write(true, true, strLog);
        }

        private void Write(bool bTime, bool bLine, string strLog)
        {
            string strTime = "";
            if (bTime)
                strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\tThreadId:" + AppDomain.GetCurrentThreadId() + "\t";

            strTime += strLog;
            if (bLine)
                strTime += "\r\n";
            WriteFile(strTime);
        }

        private string sLock = "";
        private void WriteFile(string strLog)
        {
            if (_bLog == false)
                return;

            if (_fileNameFormat == null)
            {
                return;
            }

            string strFile = GetFileName(_fileNameFormat);
            if (strFile.IndexOf(':') < 0)
                strFile = Path + "\\" + strFile;

            CreateFolder(strFile);

            lock (sLock)
            {
                FileStream stream = new FileStream(strFile, FileMode.Append, FileAccess.Write);

                byte[] bytes = Encoding.Default.GetBytes(strLog);
                stream.Write(bytes, 0, bytes.GetUpperBound(0) + 1);
                stream.Flush();
                stream.Close();
            }

        }

        private void CreateFolder(string fileName)
        {
            string pathName;
            int pos = fileName.LastIndexOf('\\');
            if (pos >= 0)
            {
                pathName = fileName.Remove(pos);
            }
            else
                pathName = fileName;
            Directory.CreateDirectory(pathName);
        }

        private string GetFileName(string fileNameFormat)
        {
            List<string> lsFileName = new List<string>();
            int pos = 0;
            int lastPos = 0;
            while (true)
            {
                string strItem;
                pos = fileNameFormat.IndexOf('<', pos);
                if (pos >= 0)
                {
                    if (pos > 0)
                    {
                        strItem = fileNameFormat.Substring(lastPos, pos - lastPos);
                        lsFileName.Add(strItem);
                        lastPos = pos;
                    }
                    pos = fileNameFormat.IndexOf('>', pos);
                    strItem = fileNameFormat.Substring(lastPos, pos - lastPos + 1);
                    lsFileName.Add(strItem);
                    lastPos = pos + 1;
                }
                else
                {
                    if (lastPos == fileNameFormat.Length)
                        break;
                    strItem = fileNameFormat.Substring(lastPos);
                    lsFileName.Add(strItem);
                    break;
                }
            }

            string strFileName = "";
            foreach (string s in lsFileName)
            {
                if (s.IndexOf('<') >= 0)
                {
                    string str = s.Replace("<", "");
                    str = str.Replace(">", "");
                    str = DateTime.Now.ToString(str);
                    strFileName += str;
                }
                else
                    strFileName += s;
            }
            return strFileName;
        }
    }
}

