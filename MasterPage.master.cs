using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook_Graph_Toolkit;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //FGT_Version.Text = System.Reflection.Assembly.GetAssembly(typeof(FacebookAppConfig)).GetName().Version.ToString();
    }
}
