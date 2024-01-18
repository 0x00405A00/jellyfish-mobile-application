using Infrastructure.Handler.Abstraction;

namespace Infrastructure.Handler.Device.ClipBoard
{
    public class ClipBoardHandler : AbstractDeviceActionHandler
    {
        public override Task CheckAndRequestPermission()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> AreRequiredPermissionsGranted()
        {
            throw new NotImplementedException();
        }
    }
}
