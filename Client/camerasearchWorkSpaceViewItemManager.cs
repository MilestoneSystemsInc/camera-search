using VideoOS.Platform.Client;

namespace camerasearch.Client
{
    public class camerasearchWorkSpaceViewItemManager : ViewItemManager
    {
        public camerasearchWorkSpaceViewItemManager() : base("camerasearchWorkSpaceViewItemManager")
        {
        }

        public override ViewItemWpfUserControl GenerateViewItemWpfUserControl()
        {
            return new camerasearchWorkSpaceViewItemWpfUserControl();
        }
    }
}
