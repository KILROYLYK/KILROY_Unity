using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using KILROY.Base;
using KILROY.Tool;

namespace KILROY.Controller
{
    public class FileController : BaseControllerBehaviour<FileController>
    {
        #region Parameter

        private string DataPath = string.Empty;
        private int Level = 9; // 压缩等级
        private int Buffer = 1024 * 4; // 限制缓冲区（减少内存占用）

        #endregion

        #region Cycle

        public void Awake()
        {
            DataPath = UnityEngine.Application.dataPath;

            // Test
            // Zip(new Dictionary<string, string>() { { "/Resources/File/Zip", "/Resources/File/Zip.zip" } });
            // Unzip(new Dictionary<string, string>() { { "/Resources/File/Zip.zip", "/Resources/File/Unzip/" } });
        }

        // public void Start() { }

        // public void Update() { }

        #endregion

        #region Zip

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <param name="rootPath">目录根地址</param>
        /// <param name="stream">数据流</param>
        /// <param name="callback">回调</param>
        private void ZipFile(string path, string rootPath, ZipOutputStream stream, Action<string> callback = null)
        {
            string fileName = path.Replace(DataPath, "");
            FN.Log("开始-文件-" + fileName);

            ZipEntry entry = new ZipEntry(path.Replace(rootPath, ""));
            byte[] buffer = new byte[Buffer];
            int fileByte = 0;

            entry.DateTime = DateTime.Now;
            stream.PutNextEntry(entry);

            using (FileStream fileStream = File.OpenRead(path))
            {
                do
                {
                    fileByte = fileStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, fileByte);
                }
                while (fileByte > 0);

                fileStream.Close();
            }

            callback?.Invoke(fileName);

            FN.Log("完成-文件-" + fileName);
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="path">文件或文件夹地址</param>
        /// <param name="rootPath">目录根地址</param>
        /// <param name="stream">数据流</param>
        /// <param name="callback">回调</param>
        private void ZipDirectory(string path, string rootPath, ZipOutputStream stream, Action<string> callback = null)
        {
            string directoryName = path.Replace(DataPath, "");
            FN.Log("开始-文件夹-" + directoryName);

            string[] fileList = Directory.GetFiles(path);
            foreach (string filePath in fileList) ZipFile(filePath, rootPath, stream, callback);

            string[] directoryList = Directory.GetDirectories(path);
            foreach (string directoryPath in directoryList) ZipDirectory(directoryPath, rootPath, stream, callback);

            FN.Log("完成-文件夹-" + directoryName);
        }

        /// <summary>
        /// 重载-压缩文件或文件夹
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="zipPath">压缩地址</param>
        /// <param name="zipCallback">压缩回调</param>
        /// <param name="finishCallback">完成回调</param>
        public void Zip(string filePath, string zipPath, Action<string> zipCallback = null, Action<string> finishCallback = null)
        {
            string path = DataPath + filePath;

            FN.Log("----------");
            FN.Log("压缩开始");
            FN.Log("压缩-" + filePath);
            FN.Log("压缩至-" + zipPath);

            try
            {
                if (File.Exists(zipPath)) File.Delete(DataPath + zipPath);

                using (FileStream zip = File.Create(DataPath + zipPath))
                {
                    using (ZipOutputStream stream = new ZipOutputStream(zip))
                    {
                        stream.SetLevel(Level);

                        ZipDirectory(path, path, stream, zipCallback);

                        stream.Finish();
                        stream.Close();
                    }

                    zip.Close();
                }

                finishCallback?.Invoke(zipPath);

                FN.Log("压缩结束-完成");
            }
            catch (Exception e)
            {
                FN.Log("压缩结束-失败-" + e.Message);
            }

            FN.Log("----------");
        }

        /// <summary>
        /// 重载-压缩文件列表或文件夹列表
        /// </summary>
        /// <param name="pathList">文件地址列表</param>
        /// <param name="zipCallback">压缩回调</param>
        /// <param name="finishCallback">完成回调</param>
        public void Zip(Dictionary<string, string> pathList, Action<string> zipCallback = null, Action<string> finishCallback = null)
        {
            foreach (KeyValuePair<string, string> pair in pathList) Zip(pair.Key, pair.Value, zipCallback, finishCallback);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="entry">文件入口</param>
        /// <param name="rootPath">目录根地址</param>
        /// <param name="stream">数据流</param>
        /// <param name="callback">回调</param>
        private void UnzipFile(ZipEntry entry, string rootPath, ZipInputStream stream, Action<string> callback = null)
        {
            string filePath = entry.Name.Replace(DataPath, "");
            FN.Log("开始-文件-" + filePath);

            string directoryName = Path.GetDirectoryName(entry.Name) + "/";
            string fileName = Path.GetFileName(entry.Name);
            string path = rootPath + directoryName;
            byte[] buffer = new byte[Buffer];
            int fileByte = 0;

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (FileStream fileStream = File.Create(path + fileName))
            {
                do
                {
                    fileByte = stream.Read(buffer, 0, buffer.Length);
                    fileStream.Write(buffer, 0, fileByte);
                }
                while (fileByte > 0);

                fileStream.Close();
            }

            callback?.Invoke(filePath);

            FN.Log("完成-文件-" + filePath);
        }

        /// <summary>
        /// 重载-解压文件或文件夹
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="unzipPath">解压地址</param>
        /// <param name="unzipCallback">解压回调</param>
        /// <param name="finishCallback">完成回调</param>
        public void Unzip(string filePath, string unzipPath, Action<string> unzipCallback = null, Action<string> finishCallback = null)
        {
            string path = DataPath + unzipPath;

            FN.Log("----------");
            FN.Log("解压开始");
            FN.Log("解压-" + filePath);
            FN.Log("解压至-" + unzipPath);

            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                using (FileStream zip = File.OpenRead(DataPath + filePath))
                {
                    using (ZipInputStream stream = new ZipInputStream(zip))
                    {
                        ZipEntry entry = null;

                        while ((entry = stream.GetNextEntry()) != null) UnzipFile(entry, path, stream, unzipCallback);

                        stream.Close();
                    }

                    zip.Close();
                }

                finishCallback?.Invoke(unzipPath);

                FN.Log("解压结束-完成");
            }
            catch (Exception e)
            {
                FN.Log("解压结束-失败-" + e.Message);
            }

            FN.Log("----------");
        }

        /// <summary>
        /// 重载-解压文件列表或文件夹列表
        /// </summary>
        /// <param name="pathList">文件地址列表</param>
        /// <param name="unzipCallback">解压回调</param>
        /// <param name="finishCallback">完成回调</param>
        public void Unzip(Dictionary<string, string> pathList, Action<string> unzipCallback = null, Action<string> finishCallback = null)
        {
            foreach (KeyValuePair<string, string> pair in pathList) Unzip(pair.Key, pair.Value, unzipCallback, finishCallback);
        }

        #endregion
    }
}