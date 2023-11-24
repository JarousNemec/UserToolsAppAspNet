namespace UserToolsApp.Models;

public class OhmViewModel
{
    public double Voltage { get; set; }
    public double Current { get; set; }
    public double Resistance { get; set; }
    public string Result { get; set; } = "resistance";
}