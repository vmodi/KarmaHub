using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageTab : Facebook_Graph_Toolkit.TabPage
{
    protected void Page_Load(object sender, EventArgs e) {
        if (Api == null) div_Authorized.Visible = false;
        else {
            div_NotAuthorized.Visible = false;
            Facebook_Graph_Toolkit.GraphApi.User u = new Facebook_Graph_Toolkit.GraphApi.User("me", Api.AccessToken);
            string name = u.Name;
            UInt64 sum = 0;
            foreach (char c in name) sum += (UInt64)c;
            Label_Sum.Text = sum.ToString();
            Label_name.Text = name;
        }
    }

    protected void AuthClick(object sender, EventArgs e) {
        RedirectToFacebookAuthorization();
    }
}