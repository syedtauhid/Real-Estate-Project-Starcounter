using Starcounter;
using System;

namespace OrganizationManagementApp
{
    partial class AddEmployee : Json, IBound<Person>
    {
        public event EventHandler Saved;

        void Handle(Input.AddEmployeeTrigger add)
        {
            PersonNo = (int)Db.SQL<long>("SELECT max(i.PersonNo) FROM Person i").First + 1;
            Transaction.Commit();
            if(CompanyNo != 0)
            {
               Data = new Person
                {
                    CompanyNo = (int)CompanyNo,
                    PersonName = ""
                };
            }
            OnSaved();
        }

        public void OnSaved()
        {
            if (this.Saved != null)
            {
                this.Saved(this, EventArgs.Empty);
            }
        }
    }
}
