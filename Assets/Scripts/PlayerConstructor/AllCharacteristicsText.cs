namespace UI.Windows.Garage
{
    public class AllCharacteristicsText
    {
        public const string Acceleration = "% � ���������";
        public const string SlowCarriage = "% � ���������� �� ���������� ��������  ";
        public const string TurnSpeedAcceleration = "% � �������� ��������";
        public const string DamageToEnemies = "% ����� ����������� ��� ������������ � �������";
        public const string DamageToPlayerFromEnem = "% ����� �� ������������ � ������������";
        public const string DamageToPlayerFromObst = "% ����� �� ������������ � �������������";
        public const string WeaponRange = "���������  ";
        public const string WeaponDamage = "����  ";
        public const string WeaponReloadTime = "�����������  ";

        public static string GetCharacteristicString(float value, string name)
        {
            string detailCharacteristic = string.Empty;
            if (value != 1)
            {
                float characteristicView = (value - 1) * 100;
                if (characteristicView >= 0)
                {
                    detailCharacteristic = "+";
                }
                detailCharacteristic = detailCharacteristic + characteristicView + name + "\n";
            }
            return detailCharacteristic;
        }

    }
}
