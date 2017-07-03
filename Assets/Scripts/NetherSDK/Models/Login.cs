using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.NetherSDK.Models
{
    [Serializable]
    public class Login
    {
        public string providerId;
        public string providerType;
        public string providerData;
    }

}
