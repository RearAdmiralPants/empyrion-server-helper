namespace EmpyrionManager.Managers
{
    using System;
    using System.Configuration;
    public class SettingManager : ApplicationSettingsBase
    {
        private const string EMPYRION_DEDICATED_SERVER_PATH = "EmpyrionDedicatedServerPath";
        private const string EMPYRION_BACKUP_DESTINATION = "EmpyrionBackupDestination";
        private const string EMPYRION_SAVES_LOCATION = "EmpyrionSavesLocation";
        private const string RESTORE_RECOVERY_DESTINATION = "RestoreRecoveryDestination";

        [UserScopedSetting()]
        [DefaultSettingValue("white")]
        public string EmpyrionDedicatedServerPath
        {
            get
            {
                return ((string)this[EMPYRION_DEDICATED_SERVER_PATH]);
            }
            set
            {
                this[EMPYRION_DEDICATED_SERVER_PATH] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("C:\\steamcmd\\empyrionBackup")]
        public string EmpyrionBackupDestination
        {
            get
            {
                return ((string)this[EMPYRION_BACKUP_DESTINATION]);
            }
            set
            {
                this[EMPYRION_BACKUP_DESTINATION] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("C:\\steamcmd\\empyriondedicatedserver\\EMPSaves")]
        public string EmpyrionServerSaveLocation
        {
            get
            {
                return ((string)this[EMPYRION_SAVES_LOCATION]);
            }
            set
            {
                this[EMPYRION_SAVES_LOCATION] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("C:\\steamcmd\\empRestoreRecovery")]
        public string EmpyrionRestoreRecoveryDirectory
        {
            get
            {
                return ((string)this[RESTORE_RECOVERY_DESTINATION]);
            }
            set
            {
                this[RESTORE_RECOVERY_DESTINATION] = value;
            }
        }
    }
}
