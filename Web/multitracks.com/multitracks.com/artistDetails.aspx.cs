using DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class artistDetails : System.Web.UI.Page
{
    public string artistName;
    public string imgUrl;
    public string heroUrl;

    public Dictionary<int, string> timeSignatures = new Dictionary<int, string>() {
        { 1, "2/4" },
        { 3, "4/4" },
        { 13, "6/8" },
        { 18, "12/8" }
    };

    public string[] tableNames = new string[]{ "Artist", "Album", "Song" };
    protected void Page_Load(object sender, EventArgs e)
    {
        var sql = new SQL();

        try
        {
            string artistId = Request.QueryString["artistId"];

            if (string.IsNullOrEmpty(artistId))
                throw new Exception("artistId does not provided");
            else
                sql.Parameters.Add("artistId", Convert.ToInt32(artistId));

            var data = sql.ExecuteStoredProcedureDS("GetArtistDetails");

            DataTableExtensions.SetTableNames(data, tableNames);

            //Table index 0 Artist
            artistName = data.Tables["Artist"].Rows[0]["title"].ToString();
            imgUrl = data.Tables["Artist"].Rows[0]["imageURL"].ToString();
            heroUrl = data.Tables["Artist"].Rows[0]["heroURL"].ToString();

            //Split to get just first paragraphs before read more tag
            string bio = data.Tables["Artist"].Rows[0]["biography"].ToString();
            string[] bioParagraphs = bio.Split(new string[] { "<!-- read more -->" }, StringSplitOptions.None);
            string[] bios = bioParagraphs[0].Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

            bioItems.DataSource = bios;
            bioItems.DataBind();

            //Table index 1 Albums
            albumItems.DataSource = data.Tables["Album"];
            albumItems.DataBind();

            //Table index 2 Songs
            songItems.DataSource = data.Tables["Song"];
            songItems.DataBind();

        }
		catch (Exception ex)
		{
            detailDiv.Visible = false;
            notFound.Visible = true;
		}
    }

    public string GetTimeSignature(int id) => timeSignatures[id];

}