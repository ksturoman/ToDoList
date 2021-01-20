using Xamarin.Forms;

namespace Artec3DSample.Models.DTO.Enums
{
    public enum TaskItemStatus
    {
        Opened = 0,
        InProgress = 1,
        Completed = 2
    }

    public static class TaskItemStatusExtensions
    {
        public static Color ToColor(this TaskItemStatus status)
        {
            switch (status)
            {
                case TaskItemStatus.InProgress:
                    return Color.FromHex("#00c853");
                case TaskItemStatus.Opened:
                    return Color.FromHex("#2962ff");
                default:
                    return Color.FromHex("#9e9e9e");
            }
        }

        public static string ToString(this TaskItemStatus status)
        {
            switch (status)
            {
                case TaskItemStatus.InProgress:
                    return "In progress";
                case TaskItemStatus.Opened:
                    return "Opened";
                case TaskItemStatus.Completed:
                    return "Completed";
                default:
                    return "Unknown";
            }
        }
    }
}
