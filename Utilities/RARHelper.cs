using System;
using System.Diagnostics;
using System.IO;

namespace Utilities
{
    public class RARHelper:System.Web.UI.Page
    {
        /// <summary>
        /// 解压缩指定的rar文件。
        /// </summary>
        /// <param name="rarFileToDecompress">rar文件（绝对路径）。</param>
        /// <param name="directoryToSave">解压缩保存的目录。</param>
        /// <param name="deleteRarFile">解压缩后删除rar文件。</param>
        public void DecompressRAR(string rarFileToDecompress, string directoryToSave, bool deleteRarFile)
        {
            string winrarExe = Server.MapPath(@"../../EnterpriseCustom/WinRaR.exe");//需要在指定路径下放入winara.exe的可执行文件在安装目录下可以找到这个文件
            if (new FileInfo(winrarExe).Exists)
            {
                directoryToSave = CheckDirectoryName(directoryToSave);
                try
                {
                    Process p = new Process();
                    // 需要启动的程序名
                    p.StartInfo.FileName = winrarExe;
                    // 参数
                    string arguments = @"x -inul -y -o+";
                    arguments += " " + rarFileToDecompress + " " + directoryToSave;

                    p.StartInfo.Arguments = arguments;

                    p.Start();//启动
                    while (!p.HasExited)
                    {
                    }
                    p.WaitForExit();
                }
                catch (Exception ee)
                {
                    throw new Exception("上传的压缩文件在解压缩的过程中出现了错误！<BR>请联系管理员检查您是否有对相应目录的写入权限！");
                }

                if (deleteRarFile)
                {
                    File.Delete(rarFileToDecompress);
                }
            }
            else
            {
                throw new Exception("系统服务器上缺少必须的Ｗinrar.exe文件，不能完成相应操作请联系管理员!");

            }
        }
        public string CheckDirectoryName(string directoryToSave)
        {
            if (Directory.Exists(directoryToSave) == false)
            {
                Directory.CreateDirectory(directoryToSave);
            }
            return directoryToSave;
        }
    }
}
