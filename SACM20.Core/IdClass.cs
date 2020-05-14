using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SACM20.Core
{
    public abstract class IdClass : IClass
    {
        private Guid m_id;

        public IdClass()
        {
            this.m_id = Guid.NewGuid();
        }
        public Guid Id
        {
            get { return this.m_id; }
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public static T Deserialize<T>(string sourceString) where T: class
        {
            sourceString = Utils.FromBase64(sourceString);

            var baseType = typeof(T);
            var constructor = baseType.GetConstructor(new Type[] { });
            if (constructor == null) return default(T);
            var obj = constructor.Invoke(null);
            var tmpLines = sourceString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in tmpLines)
            {
                string[] nv = line.Split(new char[] { '|' });
                object propValue;
                Type propType = Type.GetType(nv[2]);
                if (propType == typeof(Guid))
                    propValue = Guid.Parse(Utils.FromBase64(nv[1]));
                else if (propType.IsEnum)
                    propValue = Enum.Parse(propType, Utils.FromBase64(nv[1]));
                else
                {
                    if (!string.IsNullOrEmpty(nv[1]))
                        propValue = Convert.ChangeType(Utils.FromBase64(nv[1]), propType);
                    else
                        propValue = null;
                }
                var pInfo = typeof(T).GetProperty(nv[0]);
                pInfo.SetValue(obj, propValue);
            }

            return obj as T;
        }
    }
}
