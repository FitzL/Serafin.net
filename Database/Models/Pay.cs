namespace Serafin.NET.Database.Models
{
  public static class Pay
  {
    public readonly static int WorkingHours = 60_000 * 15;
    public readonly static int WorkingHoursVariance = 4;
    public readonly static int BasePay = 1;
    public readonly static int MaxInterestBalance = 5_000;
    public readonly static int InterestRatio = 250;
    public readonly static int LevelInterest = 10;
  }
}
