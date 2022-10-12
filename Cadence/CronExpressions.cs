namespace MtHangfire.Cadence;

public static class CronExpressions
{
    public const string EveryMinute = "0 * * * * *";
    public const string Every5Minutes = "0 */5 * * * *";
    public const string Every10Minutes = "0 */10 * * * *";
    public const string Every15Minutes = "0 */15 * * * *";
    public const string EveryHour = "0 0 * * * *";
    public const string EveryHour30MinOffset = "0 30 * * * *";
    public const string EveryOddTwoHours = "0 0 1/2 * * *";
    public const string EveryHourAt50Minutes = "0 50 * * * *";
    public const string EveryFourHours = "0 0 */4 * * *";
    public const string Never = "9000.00:00:00";

    public const string Midnight = "0 0 0 * * *";
    public const string Midnight30MinsOffset = "0 30 0 * * *";
    public const string Midnight90MinsOffset = "0 30 1 * * *";
    public const string Midnight95MinsOffset = "0 35 1 * * *";
    public const string Noon = "0 0 12 * * *";
    public const string EveryTwoWorkingHours = "0 0 6-20/2 * * *";
    public const string EveryFourHoursWeekday = "0 0 */4 * * 1-5";
    public const string EveryMonday = "0 0 0 * * 1";
    public const string EveryMonth = "0 0 0 1 * *";
    public const string EveryEightHours = "0 0 */8 * * *";

    public const string EveryFourHours5Min = "0 5 */4 * * *";
    public const string EveryFourHours10Min = "0 10 */4 * * *";
    public const string EveryFourHours15Min = "0 15 */4 * * *";
    public const string EveryFourHours20Min = "0 20 */4 * * *";
    public const string EveryFourHours25Min = "0 25 */4 * * *";
    public const string EveryFourHours30Min = "0 30 */4 * * *";
    public const string EveryFourHours35Min = "0 35 */4 * * *";
    public const string EveryFourHours40Min = "0 40 */4 * * *";
    public const string EveryFourHours45Min = "0 45 */4 * * *";
    public const string EveryFourHours50Min = "0 50 */4 * * *";
}
