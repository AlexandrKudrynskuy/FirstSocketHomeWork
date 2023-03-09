using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Servak.Model
{
    public class ObjectColor
    {
        public string Name { get; set; }
        public System.Drawing.Color color { get; set; }

        public static string Serialize(ObjectColor obColor)=> JsonConvert.SerializeObject(obColor);

        public static ObjectColor Deserialize(string str) => JsonConvert.DeserializeObject<ObjectColor>(str);


    }
}
