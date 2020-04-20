namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.1. Метод для получения информации об организации, в которой зарегистрирован текущий пользователь
    /// Формат объекта AgreementsInfo
    /// </summary>
    [DataContract]
    public class AgreementsInfo
    {
        /// <summary>
        /// Договор о присоединении
        /// </summary>
        [DataMember(Name = "contract_join", IsRequired = false)]
        public AgreementInfoEntry[] ContractJoin { get; set; }

        /// <summary>
        /// Договор о платности
        /// </summary>
        [DataMember(Name = "contract_billing", IsRequired = false)]
        public AgreementInfoEntry[] ContractBilling { get; set; }

        /// <summary>
        /// Договор о безвозмездном использовании РВ
        /// </summary>
        [DataMember(Name = "contract_withdrawal_registrator", IsRequired = false)]
        public AgreementInfoEntry[] ContractWithdrawalRegistrator { get; set; }

        /// <summary>
        /// Договор о безвозмездном использовании РЭ
        /// </summary>
        [DataMember(Name = "contract_emission_registrator", IsRequired = false)]
        public AgreementInfoEntry[] ContractEmissionRegistrator { get; set; }

        /// <summary>
        /// Договор о безвозмездном использовании РЭ с удаленным доступом
        /// </summary>
        [DataMember(Name = "contract_remote_emission_registrator", IsRequired = false)]
        public AgreementInfoEntry[] ContractRemoteEmissionRegistrator { get; set; }

        /// <summary>
        /// Заявление на предоставление оборудования (на основании 
        /// анкет на РВ, к договору о безвозмездном использовании РВ)
        /// </summary>
        [DataMember(Name = "application_withdrawal_registrator", IsRequired = false)]
        public AgreementInfoEntry[] ApplicationWithdrawalRegistrator { get; set; }

        /// <summary>
        /// Заявление на предоставление оборудования (на основании 
        /// анкет на РЭ, к договору о безвозмездном использовании РЭ)
        /// </summary>
        [DataMember(Name = "application_emission_registrator", IsRequired = false)]
        public AgreementInfoEntry[] ApplicationEmissionRegistrator { get; set; }

        /// <summary>
        /// Заявление на предоставление оборудования (на основании 
        /// анкет на РЭ с удаленным доступом, к договору о безвозмездном 
        /// использовании РЭ с удаленным доступом)
        /// </summary>
        [DataMember(Name = "application_remote_emission_registrator", IsRequired = false)]
        public AgreementInfoEntry[] ApplicationRemoteEmissionRegistrator { get; set; }
    }
}
