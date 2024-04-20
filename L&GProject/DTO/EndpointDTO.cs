namespace L_GProject.DTOs
{
    public class EndpointDTO
    {
        private string _serialNumber;
        private int _meterModelId;
        private int _meterNumber;
        private string _meterFirmwareVersion;
        private int _switchState;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }
        public int MeterModelId
        {
            get { return _meterModelId; }
            set { _meterModelId = value; }
        }
        public int MeterNumber
        {
            get { return _meterNumber; }
            set { _meterNumber = value; }
        }
        public string MeterFirmwareVersion
        {
            get { return _meterFirmwareVersion; }
            set { _meterFirmwareVersion = value; }
        }
        public int SwitchState
        {
            get { return _switchState; }
            set { _switchState = value; }
        }
    }
}
