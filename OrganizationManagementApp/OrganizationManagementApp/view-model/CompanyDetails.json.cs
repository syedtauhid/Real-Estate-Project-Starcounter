using Starcounter;

namespace OrganizationManagementApp
{
    partial class CompanyDetails : Json
    {
        public void RefreshData()
        {
            PersonList = Db.SQL<Person>("SELECT p FROM Person p WHERE NOT p.PersonName=? AND p.CompanyNo=?", string.Empty, this.CompanyNo);
        }

        [CompanyDetails_json.PersonList]
        partial class InvoicesItemPage
        {
            protected override void OnData()
            {
                base.OnData();
                this.Url = string.Format("/employee/{0}/details", this.PersonNo);
            }
        }
    }
}
