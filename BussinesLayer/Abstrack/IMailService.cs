using DataaccsessLayer.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstrack
{
    public interface IMailService: IGnericService<Mail>
    {
        List<Mail> GetMailsByWebsiteID(int websiteID);
    }
}
