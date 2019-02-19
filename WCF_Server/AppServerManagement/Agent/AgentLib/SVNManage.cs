using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLib
{
    public class SVNManage
    {
        static public SVNUpdateResult Update(string dirFullPath)
        {
            var svnResult = new SVNUpdateResult();

            try
            {
                using (var client = new SharpSvn.SvnClient())
                {
                    svnResult.IsSuccess = client.Update(dirFullPath);

                    var workingCopyClient = new SharpSvn.SvnWorkingCopyClient();
                    SharpSvn.SvnWorkingCopyVersion version;
                    workingCopyClient.GetVersion(dirFullPath, out version);
                    svnResult.Revision = version.End;
                }

                CommonLib.DevLog.Write(string.Format("App서버 SVN 업데이트 성공 여부:{0}. 리비전:{1}", svnResult.IsSuccess, svnResult.Revision), CommonLib.LOG_LEVEL.INFO);
            }
            catch(Exception ex)
            {
                CommonLib.DevLog.Write(string.Format("예외 발생: {0}", ex.ToString()), CommonLib.LOG_LEVEL.ERROR);

                svnResult.ErrorMsg = ex.ToString();
                return svnResult;
            }

            return svnResult;
        }
    }

    public class SVNUpdateResult
    {
        public bool IsSuccess;
        public long Revision;
        public string ErrorMsg;
    }

    
}
