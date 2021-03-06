#region usings
using System;
using System.ComponentModel.Composition;
using VVVV.PluginInterfaces.V2;
using VVVV.Core.Logging;
using System.Runtime.Serialization;
using VVVV.Utils;

#endregion usings

namespace VVVV.Nodes.Generic
{
	
	public class GetSlice<T> : IPluginEvaluate
	{
		#region fields & pins
        #pragma warning disable 649, 169
        [Input("Input", BinSize = 1)]
		IDiffSpread<ISpread<T>> FInput;

		[Input("Index", DefaultValue = 0)]
		ISpread<int> FIndex;

		[Output("Output", AutoFlush = false, BinVisibility = PinVisibility.Hidden)]
		ISpread<ISpread<T>> FOutput;

        [Import]
		ILogger FLogger;
		
		protected DataContractResolver FResolver = null;
        #pragma warning restore
        #endregion fields & pins

        public void Evaluate(int SpreadMax)
		{
            SpreadMax = FInput.IsAnyInvalid() ? 0 : FIndex.SliceCount;

            if (SpreadMax <= 0)
                if (FOutput.SliceCount == 0)
                {
                    FOutput.SliceCount = 0;
                    FOutput.Flush();
                    return;
                }
                else return;			
            
            
            FOutput.SliceCount = SpreadMax;
			
			for (int i=0;i<SpreadMax;i++) {
				FOutput[i].AssignFrom(FInput[FIndex[i]]);
			}

			FOutput.Flush();
		}
		
	}
	
	
}