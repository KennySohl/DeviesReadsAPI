namespace DeviesReadsAPI.Models;

public class PurchaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int RemainingStock { get; set; }
}
