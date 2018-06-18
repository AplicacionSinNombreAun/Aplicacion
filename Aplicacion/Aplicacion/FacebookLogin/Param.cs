using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Aplicacion.FacebookLogin
{

    public class Param
    {

        public object Element { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public ParamType Type { get; set; }

        public byte[] Bytes
        {
            get
            {
                if (Type == ParamType.File)
                {
                    if (Element is byte[])
                    {
                        return Element as byte[];
                    }
                    else if (Element is Stream)
                    {
                        var streamcontent = Element as Stream;
                        return ReadFully(streamcontent);
                    }
                }
                return null;
            }
        }

        public string ContentType { get; set; }

        public Param(string name, object value, ParamType type = ParamType.File, string filename = "file.jpg", string contenttype = "application/json")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("El parametro NAME es null o esta vacio");
            }

            if (value is null)
            {
                throw new Exception("El parametro VALUE es null");
            }

            Element = value;
            Name = name;
            FileName = filename;
            Type = type;
            ContentType = contenttype;
        }

        private byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }

    public enum ParamType
    {
        File, String
    }
}
