using Starcounter;
using System;

namespace OrganizationManagementApp
{
    partial class AddCompany : Json
    {
        public event EventHandler Saved;

        void Handle(Input.AddCompanyTrigger save)
        {
            CompanyNo = (int)Db.SQL<long>("SELECT max(i.CompanyNo) FROM Company i").First + 1;
            Transaction.Commit();
            OnSaved();
            Data = new Company
            {
                Name = ""
            };
        }

        protected void OnSaved()
        {
            if (this.Saved != null)
            {
                this.Saved(this, EventArgs.Empty);
            }
        }
    }
}
