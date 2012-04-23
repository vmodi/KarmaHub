using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook_Graph_Toolkit;
using Facebook_Graph_Toolkit.FacebookObjects;
using System.Collections;
using Facebook_Graph_Toolkit.GraphApi;
using System.Data.SqlClient;


public partial class _Default : CanvasPage
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        RequireLogin = true;
        Permissions.Add("publish_stream");
        Permissions.Add("read_friendlists");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsAuthorized)
        {
            NoAuthDisplay.Visible = true;
        }
        else
        {
            NoAuthDisplay.Visible = false;
            User U = new User(this.FBUserID, Api.AccessToken);

            List<Karma> karmas = GetKarmas(this.FBUserID);
            int points = 0;
            foreach (Karma karma in karmas)
            {
                points += Int32.Parse(karma.Points);
            }
            ArrayList info = new ArrayList();
            info.Add(new PositionData("Access Token", Api.AccessToken));
            info.Add(new PositionData("User ID", U.ID));
            info.Add(new PositionData("Name", U.Name));
            info.Add(new PositionData("Sex", U.Gender));
            info.Add(new PositionData("Locale", U.Locale));
            info.Add(new PositionData("KarmaPoints", points.ToString()));
            Repeater1.DataSource = info;
            Repeater1.DataBind();
            
            UserNameLbl.Text = U.Name;
            NameIDPair location = U.Location;
            UserLocaltionLbl.Text = location.Name;
            KarmaPointsLbl.Text = points.ToString();
            IList<NameIDPair> friends = GetFriends(U);
            foreach (NameIDPair friend in friends)
            {
                FriendsDDL.Items.Add(new ListItem(friend.Name, friend.ID));
            }

            List<Category> categories = GetCategories();
            foreach (Category category in categories)
            {
                CategoriesDDL.Items.Add(new ListItem(category.Name, category.ID));
            }

            KarmaDetailsGridView.DataSource = GetKarmaDetail(friends);
            KarmaDetailsGridView.DataBind();
            
        }
    }

    protected IList<NameIDPair> GetFriends(User U)
    {
        return Facebook_Graph_Toolkit.GraphApi.User.GetFriends(U.ID, Api.AccessToken);
    }
    protected List<Category> GetCategories()
    {
        List<Category> categories = new List<Category>();

        string conString = "Server=vikas-lptp;Database=KarmaHub;User ID=user_karma;Password=karma1234!;Trusted_Connection=false;";
        using (SqlConnection connection = new SqlConnection(conString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(
            "SELECT * FROM Categories",
            connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.Name = reader["Name"].ToString();
                        category.ID = reader["ID"].ToString();
                        category.Icon = reader["Icon"].ToString();

                        categories.Add(category);

                    }

                }
            }
        }

        return categories;
    }

    protected void AuthClick(object sender, EventArgs e)
    {
        Facebook_Graph_Toolkit.Helpers.IframeHelper.IframeRedirect("Default.aspx", true, true);
    }
    protected void PageTabClick(object sender, EventArgs e)
    {
        FacebookAppConfig c = FacebookAppConfig.FromWebConfig;
        string url = Dialog.GetAddPageTabUrl(c.AppID, c.CanvasAddress + "Default.aspx", DialogDisplayType.FullPage);
        Facebook_Graph_Toolkit.Helpers.IframeHelper.IframeRedirect(url, false, true);
    }

    protected List<Karma> GetKarmas(string UserId)
    {
        List<Karma> karmas = new List<Karma>();

        string conString = "Server=vikas-lptp;Database=KarmaHub;User ID=user_karma;Password=karma1234!;Trusted_Connection=false;";
        using (SqlConnection connection = new SqlConnection(conString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(string.Format(
            @"SELECT [KarmaGiveID]
      ,[KarmaRecieveID]
      ,[Points]
      ,[CategoryID]
  FROM [dbo].[KarmaMain]

  Where KarmaRecieveID ='{0}'", UserId),
            connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Karma karma = new Karma();
                        karma.CategoryID = reader["CategoryID"].ToString();
                        karma.KarmaGiveID = reader["KarmaGiveID"].ToString();
                        karma.KarmaRecieveID = reader["KarmaRecieveID"].ToString();
                        karma.Points = reader["Points"].ToString();
                        karmas.Add(karma);

                    }

                }
            }
        }

        return karmas;
    }

    protected void SubmitKarma(object sender, EventArgs e)
    {
        string categoryID = CategoriesDDL.SelectedValue;
        string toPerson = FriendsDDL.SelectedValue;
        string points = PointsLB.SelectedValue;
        string fromPerson = this.FBUserID;
        TestTxt.Text = string.Format("{0} gave {1} point in category {2} worth {3}", fromPerson, toPerson, categoryID, points);

        string conString = "Server=vikas-lptp;Database=KarmaHub;User ID=user_karma;Password=karma1234!;Trusted_Connection=false;";
        using (SqlConnection connection = new SqlConnection(conString))
        {
            connection.Open();


            using (SqlCommand command = new SqlCommand(

            string.Format(@"INSERT INTO [dbo].[KarmaMain]
           ([KarmaGiveID]
           ,[KarmaRecieveID]
           ,[Points]
           ,[CategoryID])
     VALUES
           ({0}
           ,{1}
           ,{2}
           ,{3})", fromPerson, toPerson, points, categoryID)
            ,
            connection))
            {

                command.ExecuteNonQuery();
            }
        }

    }

    public List<KarmaDetail> GetKarmaDetail(IList<NameIDPair> friends)
    {
        List<KarmaDetail> karmaDetails = new List<KarmaDetail>();

        List<Karma> karmas = GetKarmas(this.FBUserID);
        List<Category> categories = GetCategories();
        foreach (Category catergory in categories)
        {
            List<Karma> karmasCatergory = karmas.FindAll(
                delegate(Karma kar)
                {
                    return kar.CategoryID == catergory.ID;
                });
            KarmaDetail karmadetail = new KarmaDetail();
            karmadetail.CategoryName = catergory.Name;
            karmadetail.CategoryCount = karmasCatergory.Count;
            int karmaSum = 0;
            string karmaDetailStr = string.Empty;
            foreach (Karma kar in karmasCatergory)
            {
                karmaSum += Int32.Parse(kar.Points);
                karmaDetailStr = karmaDetailStr + string.Format("{0}({1});", GetFriendName(kar.KarmaGiveID,friends), kar.Points);
            }
            karmadetail.CategorySum = karmaSum;
            karmadetail.CategoryPersonPoints = karmaDetailStr;
            karmaDetails.Add(karmadetail);
        }

        return karmaDetails;
    }

    public string GetFriendName(string friendID, IList<NameIDPair> nameIdPair)
    {
        string friendName = string.Empty;
        foreach (NameIDPair nameId in nameIdPair)
        {
            if (nameId.ID == friendID)
            {
                friendName = nameId.Name;
                break;
            }
        }
        return friendName;
    }

    public class PositionData
    {

        private string property;
        private string value;

        public PositionData(string property, string value)
        {
            this.property = property;
            this.value = value;
        }
        public string Property
        {
            get
            {
                return property;
            }
        }
        public string Value
        {
            get
            {
                return value;
            }
        }
    }

    public class Category
    {
        public Category(string name, string id, string icon)
        {
            Name = name;
            ID = id;
            Icon = icon;
        }

        public Category()
        {
            Name = string.Empty;
            ID = string.Empty;
            Icon = string.Empty;
        }
        public string Name;
        public string ID;
        public string Icon;
    }

    public class Karma
    {
        public string KarmaGiveID;
        public string KarmaRecieveID;
        public string Points;
        public string CategoryID;
    }

    public class KarmaDetail
    {
        public string CategoryName { get; set; } 
        public int CategoryCount{ get; set; } 
        public int CategorySum{ get; set; } 
        public string CategoryPersonPoints{ get; set; } 
    }
    protected void DetailsLbl_Click(object sender, EventArgs e)
    {
        KarmaDetailsGridView.Visible = !KarmaDetailsGridView.Visible;
    }
    protected void AddKarmaLbl_Click(object sender, EventArgs e)
    {
        SubmitDiv.Visible = !SubmitDiv.Visible;
    }
}