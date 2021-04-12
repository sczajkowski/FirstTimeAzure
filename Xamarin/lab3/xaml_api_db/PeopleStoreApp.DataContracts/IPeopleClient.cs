using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleStoreApp.DataContracts
{
    public interface IPeopleClient
    {
        [Post("people")]
        Task AddPersonAsync([Body] Person p);
    }
}
