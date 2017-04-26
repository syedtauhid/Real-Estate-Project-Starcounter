using Starcounter;

namespace OrganizationManagementApp
{
    partial class CompanyDetails : Json
    {
        [CompanyDetails_json.PersonList]
        partial class InvoicesItemPage
        {
            protected override void OnData()
            {
                base.OnData();
                this.Url = string.Format("/companies/{0}", this.PersonNo);
            }
        }
    }
}
