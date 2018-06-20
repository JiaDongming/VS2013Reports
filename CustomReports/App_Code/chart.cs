using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for chart
/// </summary>
public class chart
{

    private string[] _colors = {
            "#a8d44f", "#4386d8", "#ff9a2e", "#8560b3", "#3cbfe3",
            "#AFD8F8", "#008E8E", "#FABD0F", "#FA6E46", "#A186BE",
            "#cc6600", "#ff7e00", "#2dd277", "#d79f9e", "#c4d6a4",
            "#b7aac8", "#1e4f91", "#85221d", "#627e29", "#3a2351",
            "#187388", "#a95c18", "#2661a7", "#ab2723", "#79a63d",
            "#644682", "#2788b3", "#d27518", "#8196ab", "#b2867b",
            "#95ae86", "#978ba3", "#ff0000", "#00ff00", "#0000ff",
            "#e080c0", "#e08040", "#ff00ff", "#00e080", "#00ffff",
            "#FFFF00", "#7FFF00", "#7B68EE", "#FF7F00", "#BBFFFF",
            "#B452CD"};

	public chart()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string getColor(int index)
    {
        int n = index % _colors.Length;
        return _colors[n];
    }
}