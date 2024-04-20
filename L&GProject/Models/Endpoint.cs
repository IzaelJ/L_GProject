namespace L_GProject.Models
{
    public class Endpoint
    {

        private string _serialNumber;
        private int _meterModelId { get; set; }
        private int _meterNumber { get; set; }
        private string _meterFirmwareVersion { get; set; }
        private int _switchState { get; set; }

        public Endpoint() { }

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
