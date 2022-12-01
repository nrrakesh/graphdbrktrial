using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Helper
{
    public class EnumHelper
    {
        public enum StreamName
        {
            CustomerDetails,
            CustomerAddressDetails,
            DeleteAddressDetails,
            CustomerFamilyMember,
            DeleteFamilyMember,
            CustomerInterest,
            QuestionResponse,
            GeneralMaster,
            InsuranceVendorPolicyMaster,
            CustomerInsurancedetails,
            CustomerInsuranceAdditionalDetail,
            CustomerInsurancePolicyDetails,
            TvamLoanApplicant,
            TvamLoanAddress,
            TvamLoanApplication,
            TvamLoanDetails,
            TvamLoanEmploymentDetails,
            TvamLoanExistingDetails,
            TvamLoanPersonalInformation,
            TvamBLPersonalDetail,
            TvamBLBussinessAndLoanDetail,
            TvamBLAddressDetail,
            TvamBLLoanApplication,
            ECCustomerDetails,
            DeleteECCustomerDetails,
            UPITransactionDetail,
            ECTvamDoctorConsultation,
            MPRAllRechargePlans,
            MPRCircle,
            MPROperator,
            MPRTransaction,
            MPRRechargePlansMasterData,
            TvamPaymentDetailsResponse,
            CustomerFamilyInsuranceDetails,
            UPICustomerAccountDetails,
            DeletePaymentDetailsResponse,
            tvamBLBussinessAndLoanDetails,
            TvamBLGeneralMaster
        }
    }
}
