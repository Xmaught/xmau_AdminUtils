using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUtilsClient.Peds
{
    class MethodsPeds : BaseScript
    {
        public MethodsPeds()
        {

        }

        public async void ChangeModel(List<object> args)
        {
            string model = args[0].ToString();
            int HashModel = API.GetHashKey(model);
            await Utils.LoadModel(HashModel);
            API.SetPlayerModel(API.PlayerId(), HashModel, 1);
            API.SetModelAsNoLongerNeeded((uint)HashModel);
        }
    }
}
