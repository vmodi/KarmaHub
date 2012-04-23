using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook_Graph_Toolkit;
using System.Collections;

public partial class Demo : CanvasPage
{
    protected void Page_PreInit(object sender, EventArgs e) {
        RequireLogin = true;
        Permissions.Add("publish_stream");
    }
    protected void Page_Load(object sender, EventArgs e) {
        Facebook_Graph_Toolkit.GraphApi.User U = new Facebook_Graph_Toolkit.GraphApi.User(this.FBUserID, Api.AccessToken);

        ArrayList info = new ArrayList();
        info.Add(new PositionData("Access Token", Api.AccessToken));
        info.Add(new PositionData("User ID", U.ID));
        info.Add(new PositionData("Name", U.Name));
        info.Add(new PositionData("Sex", U.Gender));
        info.Add(new PositionData("Locale", U.Locale));
        Repeater1.DataSource = info;
        Repeater1.DataBind();
    }
    protected void PublishClick(object sender, EventArgs e) {
        List<string> granted = GetGrantedPermissions();
        if (granted.Contains("publish_stream")) {
            try {
                Api.PostFeed("This is a sample message posted by ASP.NET Sample App.", null, null, new Facebook_Graph_Toolkit.FacebookObjects.PostAction("Try the .Net sample app", "http://apps.facebook.com/aspdotnetsample/"));
                Label1.Text = "The sample message is published.";
            }
            catch (Exception ex) { Label1.Text = "An error occured while trying to publish the message.<br />Error Details: " + ex.ToString(); }
        } else Label1.Text = "Publish failed. You did not grant the \"publish_stream\" permission";
    }
    protected void AuthDialogClick(object sender, EventArgs e) {
        RedirectToFacebookAuthorization();
    }
    public class PositionData {

        private string property;
        private string value;

        public PositionData(string property, string value) {
            this.property = property;
            this.value = value;
        }
        public string Property {
            get {
                return property;
            }
        }
        public string Value {
            get {
                return value;
            }
        }
    }
}