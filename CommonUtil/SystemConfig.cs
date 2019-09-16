using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonUtil
{
    public  class SystemConfig:Dictionary<String,String>
    {
        /// <summary>
        /// 文件名
        /// </summary>
        private String FileName { get; set; }
        /// <summary>
        /// 系统配置构造函数，默认在本地目录下Config.dat文件
        /// </summary>
        public SystemConfig()
        {
            FileName = AppDomain.CurrentDomain.BaseDirectory + "SystemConfig.dat";
            LoadFromFile();
        }

        /// <summary>
        /// 系统配置构造函数，需要传入文件名称
        /// </summary>
        /// <param name="fileName">配置文件名称</param>
        public SystemConfig(string fileName)
        {
            FileName = AppDomain.CurrentDomain.BaseDirectory + fileName;
            LoadFromFile();
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>键值</returns>
        public string GetValue(string key)
        {
            if (ContainsKey(key))
            {
                return this[key];
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        public void SetValue(string key, string value)
        {
            if (ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                Add(key, value);
            }
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        public void SaveToFile()       // Serializer
        {
            List<string> lines = new List<string>();
            foreach (KeyValuePair<string, string> kv in this)
            {
                string line = kv.Key + "=" + kv.Value;
                lines.Add(line);
            }
            File.WriteAllLines(FileName, lines.ToArray());
        }

        /// <summary>
        /// 从文件加载
        /// </summary>
        public void LoadFromFile()       // Deserializer  
        {
            if (!File.Exists(FileName))
            {
                return;
            }

            string[] lines = File.ReadAllLines(FileName);
            Clear();
            foreach (string line in lines)
            {
                string regexStr = @"(.*)=(.*)";
                Match mat = Regex.Match(line, regexStr, RegexOptions.IgnoreCase);
                if (mat.Groups.Count == 3)
                {
                    string key = mat.Groups[1].Value;
                    string value = mat.Groups[2].Value;
                    SetValue(key, value);
                }
            }
        }
    }
}
