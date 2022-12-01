using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using GraphDBIntegration.Helper;
using GraphDBIntegration.Models.Customer;
using GraphDBIntegration.Models.Insurance;
using GraphDBIntegration.Models.Loan;
using GraphDBIntegration.Models.MPR;
using GraphDBIntegration.Models.Telemedicine;
using GraphDBIntegration.Models.UPI;
using GraphDBIntegration.Services;
using System.Diagnostics;
using AutoMapper;

namespace GraphDBIntegration.Commands
{
    public class GraphClient:IGraphClient
    {
        private readonly IStringHelper _stringHelper;
        private readonly HttpClient _apiClient;
        private readonly ILogger _logger;
        private readonly ITableStorageCommands _tableClient;
        private readonly IMapper _mapper;

        public GraphClient(IMapper mapper, IStringHelper helper, IHttpClientFactory factory, IConfiguration configuration, ILogger<GraphClient> logger, ITableStorageCommands tableClient)
        {
            _stringHelper = helper;
            _apiClient = factory.CreateClient(Constants.AppConfiguration.GraphApi);
            _logger = logger;
            _tableClient = tableClient;
            _mapper = mapper;
        }
        public async Task GraphPush(ServiceBusReceivedMessage message)
        {
            HttpResponseMessage response;
            TableResult result;
            string schemaName = Constants.GraphConfig.schemaName;
            string stream = Constants.GraphConfig.stream;
            
            try
            {
                if (message.CorrelationId != null)
                {
                    switch (message.CorrelationId)
                    {
                        case nameof(EnumHelper.StreamName.CustomerDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.CustStatus != null)
                                            json.CustStatus = _stringHelper.convertCase(json.CustStatus);
                                        if (json.CreatedDate!=null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdateDate!=null)
                                            json.UpdateDate = _stringHelper.convertDate(json.UpdateDate);
                                        if(json.DOB!=null)
                                            json.DOB = _stringHelper.convertDate(json.DOB);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerDetails), json.CustRefID, Constants.AppConfiguration.CustomerQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.CustomerDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex} {ex.Message} {message.CorrelationId} {message.Body} ");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.CustomerAddressDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerAddressDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.AddressType != null)
                                            json.AddressType = _stringHelper.convertCase(json.AddressType);
                                        json.Status = _stringHelper.convertCase(Constants.ConstantData.Active);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdateDate != null)
                                            json.UpdateDate = _stringHelper.convertDate(json.UpdateDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerAddressDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerAddressDetails), json.AddRefID, Constants.AppConfiguration.CustomerQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.CustomerAddressDetails} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {nameof(json.AddRefID)} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex}{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.DeleteAddressDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerAddressDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.AddressType != null)
                                            json.AddressType = _stringHelper.convertCase(json.AddressType);
                                        json.Status = _stringHelper.convertCase(Constants.ConstantData.Inactive);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdateDate != null)
                                            json.UpdateDate = _stringHelper.convertDate(json.UpdateDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerAddressDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(EnumHelper.StreamName.DeleteAddressDetails.ToString(), json.AddRefID, Constants.AppConfiguration.CustomerQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.CustomerAddressDetails} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {nameof(json.AddRefID)} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex}{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.CustomerFamilyMember):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerFamilyMember>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdateDate != null)
                                            json.UpdateDate = _stringHelper.convertDate(json.UpdateDate);//Change to be noted
                                        if (json.DOB != null)
                                            json.DOB = _stringHelper.convertDate(json.DOB);
                                        json.Status = _stringHelper.convertCase(Constants.ConstantData.Active);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerFamilyMember}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerFamilyMember), json.MemberID, Constants.AppConfiguration.CustomerQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.CustomerFamilyMember} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                        
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body} \n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.DeleteFamilyMember):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerFamilyMember>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdateDate != null)
                                            json.UpdateDate = _stringHelper.convertDate(json.UpdateDate);//Change to be noted
                                        if (json.DOB != null)
                                            json.DOB = _stringHelper.convertDate(json.DOB);
                                        json.Status = _stringHelper.convertCase(Constants.ConstantData.Inactive);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerFamilyMember}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(EnumHelper.StreamName.DeleteFamilyMember.ToString(), json.MemberID, Constants.AppConfiguration.CustomerQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.CustomerFamilyMember} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{nameof(CustomerFamilyMember)} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body} \n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.CustomerInterest):
                            {
                                try
                                {

                                    var json = JsonConvert.DeserializeObject<List<CustomerInterest>>(Encoding.UTF8.GetString(message.Body));
                                    foreach (CustomerInterest custInterest in json)
                                    {
                                        if(custInterest.createddate!=null)
                                            custInterest.createddate = _stringHelper.convertDate(custInterest.createddate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerInterest}", new StringContent(JsonConvert.SerializeObject(custInterest), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerInterest), custInterest.CustRefID, Constants.AppConfiguration.CustomerQueue, JsonConvert.SerializeObject(custInterest));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.CustomerInterest} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(custInterest)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body} \n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.QuestionResponse):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<QuestionResponse>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Anniversary != null)
                                            json.Anniversary = _stringHelper.convertDate(json.Anniversary);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.QuestionResponse}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(QuestionResponse), json.CustomerId, Constants.AppConfiguration.NudgeQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.QuestionResponse} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.GeneralMaster):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<GeneralMaster>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        var newJson = _mapper.Map<GeneralMasterNew>(json);
                                        newJson.MasType = json.Type;
                                        if (newJson.CreatedDate != null)
                                            newJson.CreatedDate = _stringHelper.convertDate(newJson.CreatedDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.GeneralMaster}", new StringContent(JsonConvert.SerializeObject(newJson), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(GeneralMaster), json.ID, Constants.AppConfiguration.CustomerQueue, JsonConvert.SerializeObject(newJson));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.GeneralMaster} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(newJson)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.CustomerInsurancedetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerInsurancedetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.PolicyType != null)
                                            json.PolicyType = _stringHelper.convertCase(json.PolicyType);
                                        if (json.DOB != null)
                                            json.DOB = _stringHelper.convertDate(json.DOB);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerInsurancedetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerInsurancedetails), json.InsuranceID, Constants.AppConfiguration.InsuranceQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.CustomerInsurancedetails} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.CustomerInsuranceAdditionalDetail):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerInsuranceAdditionalDetail>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerInsuranceAdditionalDetail}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerInsuranceAdditionalDetail), json.InsuranceID, Constants.AppConfiguration.InsuranceQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.CustomerInsuranceAdditionalDetail} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body} \n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.CustomerInsurancePolicyDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerInsurancePolicyDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if(json.PolicyStatus!=null)
                                            json.PolicyStatus = _stringHelper.convertCase(json.PolicyStatus);
                                        if (json.PolicyType != null)
                                            json.PolicyType = _stringHelper.convertCase(json.PolicyType);
                                        if (json.CoverType != null)
                                            json.CoverType = _stringHelper.convertCase(json.CoverType);
                                        if (json.PolicyFor != null)
                                            json.PolicyFor = _stringHelper.convertCase(json.PolicyFor);
                                        if (json.CreatedDate!=null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if(json.PolicyCreationDate!=null)
                                            json.PolicyCreationDate = _stringHelper.convertDate(json.PolicyCreationDate);
                                        if(json.PolicyCommencementDt!=null)
                                            json.PolicyCommencementDt = _stringHelper.convertDate(json.PolicyCommencementDt);
                                        if(json.PolicyMaturityDt!=null)
                                            json.PolicyMaturityDt = _stringHelper.convertDate(json.PolicyMaturityDt);
                                        if(json.PolicyModifyDate!=null)
                                            json.PolicyModifyDate = _stringHelper.convertDate(json.PolicyModifyDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerInsurancePolicyDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerInsurancePolicyDetails), json.InsuranceID, Constants.AppConfiguration.InsuranceQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.CustomerInsurancePolicyDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body} \n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.CustomerFamilyInsuranceDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<CustomerFamilyInsuranceDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.PolicyType != null)
                                            json.PolicyType = _stringHelper.convertCase(json.PolicyType);
                                        if (json.DOB != null)
                                            json.DOB = _stringHelper.convertDate(json.DOB);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.CustomerFamilyInsuranceDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(CustomerFamilyInsuranceDetails), json.InsuranceID, Constants.AppConfiguration.InsuranceQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.CustomerFamilyInsuranceDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body} \n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.ECCustomerDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<ECCustomerDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if(json.PolicyStatus!=null)
                                            json.PolicyStatus = _stringHelper.convertCase(json.PolicyStatus);
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.CurrentPolicyStatus!=null)
                                            json.CurrentPolicyStatus = _stringHelper.convertCase(json.CurrentPolicyStatus);
                                        if (json.CoverType != null)
                                            json.CoverType = _stringHelper.convertCase(json.CoverType);
                                        if (json.Is_EConsultation != null)
                                            json.Is_EConsultation = _stringHelper.convertCase(json.Is_EConsultation);
                                        if(json.IsBookConsultationAllowed !=null)
                                            json.IsBookConsultationAllowed = _stringHelper.convertCase(json.IsBookConsultationAllowed);
                                        if (json.DateOfBirth != null)
                                            json.DateOfBirth = _stringHelper.convertDate(json.DateOfBirth);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdatedDate != null)
                                            json.UpdatedDate = _stringHelper.convertDate(json.UpdatedDate);
                                        if(json.PolicyCommencementDt!=null)
                                            json.PolicyCommencementDt = _stringHelper.convertDate(json.PolicyCommencementDt);
                                        if(json.PolicyMaturityDt!=null)
                                            json.PolicyMaturityDt = _stringHelper.convertDate(json.PolicyMaturityDt);


                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.ECCustomerDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(ECCustomerDetails), json.Id, Constants.AppConfiguration.TelemedicineQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.ECCustomerDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        
                        case nameof(EnumHelper.StreamName.DeleteECCustomerDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<ECCustomerDetails>(Encoding.UTF8.GetString(message.Body));
                                    if(json!=null)
                                    {
                                        
                                        json.PolicyStatus = _stringHelper.convertCase(Constants.ConstantData.Inactive);
                                        json.CurrentPolicyStatus = _stringHelper.convertCase(Constants.ConstantData.Inactive);
                                        if (json.CoverType != null)
                                            json.CoverType = _stringHelper.convertCase(json.CoverType);
                                        if (json.Is_EConsultation != null)
                                            json.Is_EConsultation = _stringHelper.convertCase(json.Is_EConsultation);
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.IsBookConsultationAllowed != null)
                                            json.IsBookConsultationAllowed = _stringHelper.convertCase(json.IsBookConsultationAllowed);
                                        if (json.DateOfBirth != null)
                                            json.DateOfBirth = _stringHelper.convertDate(json.DateOfBirth);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdatedDate != null)
                                            json.UpdatedDate = _stringHelper.convertDate(json.UpdatedDate);
                                        if (json.PolicyCommencementDt != null)
                                            json.PolicyCommencementDt = _stringHelper.convertDate(json.PolicyCommencementDt);
                                        if (json.PolicyMaturityDt != null)
                                            json.PolicyMaturityDt = _stringHelper.convertDate(json.PolicyMaturityDt);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.ECCustomerDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(EnumHelper.StreamName.DeleteECCustomerDetails.ToString(), json.Id, Constants.AppConfiguration.TelemedicineQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.ECCustomerDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{nameof(ECCustomerDetails)} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch(Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.ECTvamDoctorConsultation):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<ECTvamDoctorConsultation>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Type != null)
                                            json.Type = _stringHelper.convertCase(json.Type);
                                        if (json.UserGender != null)
                                            json.UserGender = _stringHelper.convertCase(json.UserGender);
                                        if (json.Status != null)
                                            json.Status = _stringHelper.convertCase(json.Status);
                                        if(json.PaymentStatus!=null)
                                            json.PaymentStatus = _stringHelper.convertCase(json.PaymentStatus);
                                        if (json.UserDob != null)
                                            json.UserDob = _stringHelper.convertDate(json.UserDob);
                                        if (json.Createddate != null)
                                            json.Createddate = _stringHelper.convertDate(json.Createddate);
                                        if (json.Updatetdate != null)
                                            json.Updatetdate = _stringHelper.convertDate(json.Updatetdate);
                                        if (json.ConsultationDate != null)
                                            json.ConsultationDate = _stringHelper.convertDate(json.ConsultationDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.ECTvamDoctorConsultation}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(ECTvamDoctorConsultation), json.TvamRefNo, Constants.AppConfiguration.TelemedicineQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.ECTvamDoctorConsultation} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamPaymentDetailsResponse):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamPaymentDetailsResponse>(Encoding.UTF8.GetString(message.Body));
                                    if(json!=null)
                                    {
                                        if (json.BankName != null)
                                            json.BankName = _stringHelper.convertCase(json.BankName);
                                        if (json.PGMode != null)
                                            json.PGMode = _stringHelper.convertCase(json.PGMode);
                                        if (json.Status != null)
                                            json.Status = _stringHelper.convertCase(json.Status);
                                        if (json.ServiceName != null)
                                            json.ServiceName = _stringHelper.convertCase(json.ServiceName);
                                        if (json.PaymentGateway != null)
                                            json.PaymentGateway = _stringHelper.convertCase(json.PaymentGateway);
                                        if (json.TxnDateTime != null)
                                            json.TxnDateTime = _stringHelper.convertDate(json.TxnDateTime);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdatedDate != null)
                                            json.UpdatedDate = _stringHelper.convertDate(json.UpdatedDate);
                                        if (json.Refunddatetime != null)
                                            json.TxnDateTime = _stringHelper.convertDate(json.Refunddatetime);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamPaymentDetailsResponse}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamPaymentDetailsResponse), json.PaymentId, Constants.AppConfiguration.PaymentQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamPaymentDetailsResponse} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.DeletePaymentDetailsResponse):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamPaymentDetailsResponse>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        
                                        json.Status = _stringHelper.convertCase(Constants.ConstantData.Inactive);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdatedDate != null)
                                            json.UpdatedDate = _stringHelper.convertDate(json.UpdatedDate);
                                        if (json.TxnDateTime != null)
                                            json.TxnDateTime = _stringHelper.convertDate(json.TxnDateTime);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamPaymentDetailsResponse}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(EnumHelper.StreamName.DeletePaymentDetailsResponse.ToString(), json.PaymentId, Constants.AppConfiguration.PaymentQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamPaymentDetailsResponse} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                        
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamBLGeneralMaster):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamBLGeneralMaster>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        var newJson = _mapper.Map<TvamBLGeneralMasterNew>(json);
                                        newJson.MasType = json.Type;
                                        if (newJson.CreatedDate != null)
                                            newJson.CreatedDate = _stringHelper.convertDate(newJson.CreatedDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamBLGeneralMaster}", new StringContent(JsonConvert.SerializeObject(newJson), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamBLGeneralMaster), json.ID, Constants.AppConfiguration.BLQueue, JsonConvert.SerializeObject(newJson));
                                        _logger.LogInformation($"{result.HttpStatusCode}  {response.StatusCode} {EnumHelper.StreamName.TvamBLGeneralMaster} {response.Content.ReadAsStringAsync().Result}  {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamBLAddressDetail):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<List<TvamBLAddressDetail>>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        foreach(var addressdetail in json)
                                        {
                                            if (addressdetail.ResidentTypeId != null)
                                                addressdetail.ResidentTypeId = _stringHelper.convertCase(addressdetail.ResidentTypeId);
                                            response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamBLAddressDetail}", new StringContent(JsonConvert.SerializeObject(addressdetail), Encoding.UTF8, "application/json"));
                                            result = await _tableClient.TableStoragePush(nameof(TvamBLAddressDetail), addressdetail.LoanApplicationId, Constants.AppConfiguration.BLQueue, JsonConvert.SerializeObject(addressdetail));
                                            _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamBLAddressDetail} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                            //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                        }
                                        
                                    }

                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamBLBussinessAndLoanDetail):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamBLBussinessAndLoanDetail>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.LoanPurposeId != null)
                                            json.LoanPurposeId = _stringHelper.convertCase(json.LoanPurposeId);
                                        if (json.BussinessTypeId != null)
                                            json.BussinessTypeId = _stringHelper.convertCase(json.BussinessTypeId);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdateDate != null)
                                            json.UpdateDate = _stringHelper.convertDate(json.UpdateDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamBLBussinessAndLoanDetail}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamBLBussinessAndLoanDetail), json.LoanApplicationId, Constants.AppConfiguration.BLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamBLBussinessAndLoanDetail} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamBLLoanApplication):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamBLLoanApplication>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.LoanApplicationStatus != null)
                                            json.LoanApplicationStatus = _stringHelper.convertCase(json.LoanApplicationStatus);
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);
                                        if (json.LoanStatusDate != null)
                                            json.LoanStatusDate = _stringHelper.convertDate(json.LoanStatusDate);
                                        if (json.NextInstalmentDate != null)
                                            json.NextInstalmentDate = _stringHelper.convertDate(json.NextInstalmentDate);
                                        if (json.LastPaymentDate != null)
                                            json.LastPaymentDate = _stringHelper.convertDate(json.LastPaymentDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamBLLoanApplication}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamBLLoanApplication), json.LoanApplicationId, Constants.AppConfiguration.BLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamBLLoanApplication} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamBLPersonalDetail):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamBLPersonalDetail>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.Gender != null)
                                            json.Gender = _stringHelper.convertCase(json.Gender);
                                        if (json.DOB != null)
                                            json.DOB = _stringHelper.convertDate(json.DOB);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.UpdateDate != null)
                                            json.UpdateDate = _stringHelper.convertDate(json.UpdateDate);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamBLPersonalDetail}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamBLPersonalDetail), json.LoanApplicationId, Constants.AppConfiguration.BLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamBLPersonalDetail} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamLoanAddress):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamLoanAddress>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamLoanAddress}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamLoanAddress), json.LoanApplicationId, Constants.AppConfiguration.PLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamLoanAddress} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamLoanApplicant):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamLoanApplicant>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamLoanApplicant}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamLoanApplicant), json.LoanApplicationId, Constants.AppConfiguration.PLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamLoanApplicant} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamLoanApplication):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamLoanApplication>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.LoanApplicationStatus != null)
                                            json.LoanApplicationStatus = _stringHelper.convertCase(json.LoanApplicationStatus);
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamLoanApplication}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamLoanApplication), json.LoanApplicationId, Constants.AppConfiguration.PLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamLoanApplication} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamLoanDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamLoanDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.PurposeOfLoan != null)
                                            json.PurposeOfLoan = _stringHelper.convertCase(json.PurposeOfLoan);
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamLoanDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamLoanDetails), json.LoanApplicationId, Constants.AppConfiguration.PLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamLoanDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamLoanEmploymentDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamLoanEmploymentDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.IndustryType != null)
                                            json.IndustryType = _stringHelper.convertCase(json.IndustryType);
                                        if (json.Designation != null)
                                            json.Designation = _stringHelper.convertCase(json.Designation);
                                        if (json.IncomeMode != null)
                                            json.IncomeMode = _stringHelper.convertCase(json.IncomeMode);
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamLoanEmploymentDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamLoanEmploymentDetails), json.LoanApplicationId, Constants.AppConfiguration.PLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamLoanEmploymentDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamLoanExistingDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamLoanExistingDetails>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.FinancialInstitution != null)
                                            json.FinancialInstitution = _stringHelper.convertCase(json.FinancialInstitution);
                                        if (json.LoanFacilityType != null)
                                            json.LoanFacilityType = _stringHelper.convertCase(json.LoanFacilityType);
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamLoanExistingDetails}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamLoanExistingDetails), json.LoanApplicationId, Constants.AppConfiguration.PLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamLoanExistingDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.TvamLoanPersonalInformation):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<TvamLoanPersonalInformation>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.ResidentialTypeId != null)
                                            json.ResidentialTypeId = _stringHelper.convertCase(json.ResidentialTypeId);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.TvamLoanPersonalInformation}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(TvamLoanPersonalInformation), json.LoanApplicationId, Constants.AppConfiguration.PLQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.TvamLoanPersonalInformation} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.MPRTransaction):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<MPRTransaction>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.RechargeType != null)
                                            json.RechargeType = _stringHelper.convertCase(json.RechargeType);
                                        if (json.PlanValidity != null)
                                            json.PlanValidity = _stringHelper.convertCase(json.PlanValidity);
                                        if (json.DataLimit != null)
                                            json.DataLimit = _stringHelper.convertCase(json.DataLimit);
                                        if (json.MobileRechargeStatus != null)
                                            json.MobileRechargeStatus = _stringHelper.convertCase(json.MobileRechargeStatus);
                                        if (json.CreatedDate != null)
                                            json.CreatedDate = _stringHelper.convertDate(json.CreatedDate);
                                        if (json.PlanStartDate != null)
                                            json.PlanStartDate = _stringHelper.convertDate(json.PlanStartDate);
                                        if (json.PlanEndDate != null)
                                            json.PlanEndDate = _stringHelper.convertDate(json.PlanEndDate);
                                        if (json.PaymentDate != null)
                                            json.PaymentDate = _stringHelper.convertDate(json.PaymentDate);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.MPRTransaction}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(MPRTransaction), json.TransactionID, Constants.AppConfiguration.MPRQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.MPRTransaction} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.MPRRechargePlansMasterData):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<MPRRechargePlansMasterData>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.CreatedDateTime != null)
                                            json.CreatedDateTime = _stringHelper.convertDate(json.CreatedDateTime);
                                        if (json.VendorPlanUpdatedAt != null)
                                            json.VendorPlanUpdatedAt = _stringHelper.convertDate(json.VendorPlanUpdatedAt);

                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.MPRRechargePlansMasterData}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(MPRRechargePlansMasterData), json.RechargeId, Constants.AppConfiguration.MPRPlansQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.MPRRechargePlansMasterData} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.UPITransactionDetail):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<UPITransactionDetail>(Encoding.UTF8.GetString(message.Body));
                                    if (json != null)
                                    {
                                        if (json.PaymentType != null)
                                            json.PaymentType = _stringHelper.convertCase(json.PaymentType);
                                        if (json.TransactionType != null)
                                            json.TransactionType = _stringHelper.convertCase(json.TransactionType);
                                        if (json.MerchantCatCode != null)
                                            json.MerchantCatCode = _stringHelper.convertCase(json.MerchantCatCode);
                                        if (json.Status != null)
                                        {
                                            if (json.Status.Equals("F",StringComparison.OrdinalIgnoreCase) || json.Status.Equals("FAILED", StringComparison.OrdinalIgnoreCase) || json.Status.Equals("FAILURE", StringComparison.OrdinalIgnoreCase))
                                                json.Status = Constants.UPIStatus.Failure;
                                            else if (json.Status.Equals("P", StringComparison.OrdinalIgnoreCase) || json.Status.Equals("PENDING", StringComparison.OrdinalIgnoreCase))
                                                json.Status = Constants.UPIStatus.Pending;
                                            else if (json.Status.Equals("S", StringComparison.OrdinalIgnoreCase) || json.Status.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
                                                json.Status = Constants.UPIStatus.Success;
                                            else if (json.Status.Equals("T", StringComparison.OrdinalIgnoreCase) || json.Status.Equals("TIMEOUT",StringComparison.OrdinalIgnoreCase))
                                                json.Status = Constants.UPIStatus.Timeout;
                                            else
                                                json.Status = _stringHelper.convertCase(json.Status);
                                        }
                                        if (json.PaymentTypeCategory != null)
                                            json.PaymentTypeCategory = _stringHelper.convertCase(json.PaymentTypeCategory);
                                        if (json.TransactionAuthDate != null)
                                            json.Createddate = _stringHelper.convertDate(json.TransactionAuthDate);
                                        if (json.Createddate != null)
                                            json.Createddate = _stringHelper.convertDate(json.Createddate);
                                        if (json.UpdatedDateTime != null)
                                            json.UpdatedDateTime = _stringHelper.convertDate(json.UpdatedDateTime);
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}/{EnumHelper.StreamName.UPITransactionDetail}", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                                        result = await _tableClient.TableStoragePush(nameof(UPITransactionDetail), json.TransactionId, Constants.AppConfiguration.UpiQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.UPITransactionDetail}  {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId} {JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex.Message} {message.CorrelationId} {message.Body}\n");
                                }
                                break;
                            }
                        case nameof(EnumHelper.StreamName.UPICustomerAccountDetails):
                            {
                                try
                                {
                                    var json = JsonConvert.DeserializeObject<UPICustomerAccountDetails>(Encoding.UTF8.GetString(message.Body));
                                    if(json!=null)
                                    {
                                        response = await _apiClient.PostAsync($"{stream}/{schemaName}{EnumHelper.StreamName.UPICustomerAccountDetails}", new StringContent(JsonConvert.SerializeObject(json)));
                                        result = await _tableClient.TableStoragePush(nameof(UPICustomerAccountDetails), json.TvamCustRefId, Constants.AppConfiguration.UpiQueue, JsonConvert.SerializeObject(json));
                                        _logger.LogInformation($"{result.HttpStatusCode} {response.StatusCode} {EnumHelper.StreamName.UPICustomerAccountDetails} {response.Content.ReadAsStringAsync().Result} {JsonConvert.SerializeObject(json)}");
                                        //_logger.LogInformation($"{message.CorrelationId}{JsonConvert.SerializeObject(json)}");
                                    }
                                }
                                catch(Exception ex)
                                {
                                    _logger.LogError($"{ex.Message}{message.CorrelationId}{message.Body} \n");
                                }
                                break;
                            }
                        default:
                            {
                                _logger.LogError($"{message.CorrelationId} Not found {message.Body}");
                                break;
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}