	



using System;
using System.Collections.Generic; 
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Threading.Tasks;


namespace WCFGameServerLib
{
	[ServiceContract]
	public interface IServerService
	{
 
		[OperationContract]
		Task<RES_DATA> RequestCreateAccountDev(REQ_DATA requestData);


 
		[OperationContract]
		Task<RES_DATA> RequestLoadUserData(REQ_DATA requestData);


 
		[OperationContract]
		Task<RES_DATA> RequestWorldExtension(REQ_DATA requestData);






		[OperationContract] 
		void RequestHeathCheck();
		  
		
	}
}