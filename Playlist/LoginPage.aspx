<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" Title="Login" %>
<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="iZoom" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="LoginPanel" Visible="false" runat="server">
        <!-- Pagetitle -->
        <iZoom:PageTitle runat="server" ID="pageTitle2" TitleText="Sign In" DividerHeight="4" TitleSize="Largest" /><br />
        <iZoom:SectionHeader Text="Sign in to get free stuff!" ID="signInSectionHeader" runat="server" />
            <asp:Login 
                ID="LoginControl" 
                runat="server" 
                DestinationPageUrl="default.aspx"
                PasswordRecoveryText="Forgot Password"
                PasswordRecoveryUrl="~/loginpage.aspx?action=recover" 
                CreateUserUrl="~/loginpage.aspx?action=create" 
                OnLoggedIn="LoginControl_LoggedIn"
                OnLoginError="LoginControl_LoginError" 
                FailureTextStyle-CssClass="errorText"
                >
             <LayoutTemplate>
             <asp:Table runat="server" ID="loginTable">
                <asp:TableRow>
                    <asp:TableCell> 
                        <asp:Label CssClass="top" ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox TabIndex="0" runat="server" id="UserName" CssClass="field" CausesValidation="true"/>&nbsp;<asp:RequiredFieldValidator ValidationGroup="LoginControl" ID="UserNameRequired" runat="server" ToolTip="Username is required." ControlToValidate="UserName" ErrorMessage="Username is required.">Username is required.</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell> 
                        <asp:Label CssClass="top" ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell> 
                        <asp:TextBox ID="Password" runat="server" CssClass="field" TextMode="Password" CausesValidation="true"></asp:TextBox>&nbsp;                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="LoginControl" >Password is required.</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button CssClass="button" ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginControl" CausesValidation="true" /><br />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me." CssClass="checkbox" CausesValidation="true" /><br />
                    </asp:TableCell>
                </asp:TableRow>
	    	    <asp:TableRow>
	    	        <asp:TableCell ColumnSpan="2"><a href="loginpage.aspx?action=recover" id="forgotpsswd">Password forgotten?</a></asp:TableCell>
	    	    </asp:TableRow>
	    	    <asp:TableRow>
	    	        <asp:TableCell ColumnSpan="2">Don't have an account? <a href="loginpage.aspx?action=register" id="register">Register now, it's free!</a></asp:TableCell>
	    	    </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
             </LayoutTemplate>
                <FailureTextStyle CssClass="error" />
            </asp:Login>
    </asp:Panel>
    
    <asp:Panel ID="RecoverPanel" Visible="false" runat="server" >
        <!-- Pagetitle -->
        <iZoom:PageTitle runat="server" ID="pageTitle3" TitleText="Password Recovery" DividerHeight="4" TitleSize="Largest" />
        <asp:PasswordRecovery  runat="server" ID="passwordRecover" OnSendingMail="PasswordRecover_OnSendingMail" OnVerifyingAnswer="PasswordRecover_OnVerifyingAnswer"  >
            <QuestionTemplate>
            <asp:Panel ID="QuestionPanel" runat="server" Width="80%" >
                <iZoom:SectionHeader ID="SectionHeader1" runat="server" Text="Identity Confirmation" />
                <p>Answer the following question to recieve your password.</p>
                <div class="subform">
                    <fieldset>
                        <p><br /><asp:Label CssClass="top" ID="UserNameLabel" runat="server" Text="Email:" /><br />
                           <span style="font-weight:bold;"><asp:Literal ID="UserName" runat="server"></asp:Literal></span></p>
                        <p><asp:Label CssClass="top" ID="QuestionLabel" runat="server" Text="Question:" /><br />
                           <span style="font-weight:bold;"><asp:Literal ID="Question" runat="server"></asp:Literal></span></p>
                        <p><asp:Label CssClass="top" ID="AnswerLabel" runat="server" AssociatedControlID="Answer" Text="Answer:" /><br />
                           <asp:TextBox CssClass="field" ID="Answer" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                ErrorMessage="Answer is required." ToolTip="Answer is required." ValidationGroup="PasswordRecoverQuestion">*</asp:RequiredFieldValidator></p>
                        <p><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></p>
                        <p><asp:Button CssClass="button" ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecoverQuestion" />&nbsp;<asp:Button CssClass="button" OnCommand="CancelButton_OnCommand" ID="CancelButton" runat="server" CommandName="CancelRecover" Text="Cancel" CausesValidation="false" /></p>
                    </fieldset>
                </div>
             </asp:Panel>
            </QuestionTemplate>
            <UserNameTemplate>
           	<asp:Panel ID="UserNamePanel" runat="server" Width="80%">
                <iZoom:SectionHeader ID="SectionHeader2" runat="server" Text="Password Recovery" />
                <p>Enter your Email to receive your password.</p>
                <div class="subform">
                    <fieldset>
                        <p><br /><asp:Label CssClass="top" ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Email:</asp:Label><br />
                        <asp:TextBox CssClass="field" ID="UserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="PasswordRecoverUserName">*</asp:RequiredFieldValidator></p>
                        <p><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></p>
                        <p><asp:Button CssClass="button" ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecoverUserName" />&nbsp;<asp:Button CssClass="button" ID="CancelButton" runat="server" OnCommand="CancelButton_OnCommand" CommandName="CancelRecover" Text="Cancel" CausesValidation="false" /></p>
                    </fieldset>
                </div>
             </asp:Panel>
            </UserNameTemplate>
            <SuccessTemplate>
                <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td>
                                        Your password has been sent to you.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </SuccessTemplate>
        </asp:PasswordRecovery>
    </asp:Panel>

    <asp:Panel Visible="false" ID="RegisterPanel" runat="server">
        <asp:CreateUserWizard  
            CancelButtonType="button" 
            CancelButtonStyle-CssClass="wrappedButton" 
            CellPadding="0" 
            CellSpacing="0" 
            NavigationStyle-HorizontalAlign="left" 
            ID="CreateUserWizard1" 
            runat="server" 
            BorderStyle="None" 
            CancelDestinationPageUrl="~/default.aspx"
            ContinueDestinationPageUrl="~/Default.aspx" 
            DisplayCancelButton="True" OnCreatingUser="CreateUserWizard1_CreatingUser" 
            OnCreateUserError="CreateUserWizard1_CreateUserError" 
            PasswordHintText="Enter at least 6 characters."
            RequireEmail="False"  
            CreateUserButtonText="Register"
            CreateUserButtonStyle-CssClass="wrappedButton"
            ContinueButtonStyle-CssClass="wrappedButton"
            FinishCompleteButtonStyle-CssClass="wrappedButton"
            UserNameRequiredErrorMessage="Please enter at least one character for the user name.">
            <wizardsteps>
                        <asp:CreateUserWizardStep runat="server" Title="Account Registration" >
                            <ContentTemplate>
    				<asp:Panel Width="80%" ID="RegistrationPanel" Visible="true" runat="server">
                            
                                <iZoom:PageTitle runat="server" ID="pageTitle" TitleText="Account Registration" DividerHeight="4" TitleSize="Largest" /><br />
                                <iZoom:SectionHeader ID="SectionHeader3" runat="server" Text="Basic Information" />
                                <div class="subform">
                                    <fieldset>
                                        <p><asp:Label CssClass="top" ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User ID:</asp:Label><br />
                                        <asp:TextBox runat="server" id="UserName" tabindex="1" CssClass="field" /><asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ToolTip="User Id is required."
                                                ErrorMessage="User Id is required." ValidationGroup="CreateUserWizard1" ControlToValidate="UserName">*</asp:RequiredFieldValidator></p>
                                        <p><asp:Label CssClass="top" ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label><br />
                                        <asp:TextBox runat="server" id="Password" tabindex="2" CssClass="field" TextMode="password" /><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ToolTip="Password cannot be blank."
                                                ErrorMessage="Password is required." ValidationGroup="CreateUserWizard1" ControlToValidate="UserName">*</asp:RequiredFieldValidator><br />
                                        <span style="width:50px; font-size:xx-small; font-style:italic;">Enter at least 6 characters.</span></p>
                                        <p><asp:Label CssClass="top" ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Re-Type Password:</asp:Label><br />
                                        <asp:TextBox runat="server" id="ConfirmPassword" tabindex="3" CssClass="field" TextMode="password" /><asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator1" 
                                                runat="server" 
                                                ToolTip="Password cannot be blank."
                                                ErrorMessage="Password is required." 
                                                ValidationGroup="CreateUserWizard1" 
                                                ControlToValidate="UserName">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator 
                                                ID="PasswordsEqual" 
                                                runat="server"
                                                ToolTip="Passwords must match."
                                                ValidationGroup="CreateUserWizard1" 
                                                ErrorMessage="Passwords must match." 
                                                ControlToCompare="ConfirmPassword" 
                                                ControlToValidate="Password">*</asp:CompareValidator></p>
                                     </fieldset>
                                </div>

                                <br />    
                                <br />
                                <iZoom:SectionHeader ID="SectionHeader4" runat="server" Text="Security Information" />
                                <div class="subform">
                                    <fieldset>
                                        <p>
                                            <br />
                                            <asp:DropDownList TabIndex="4" CssClass="field" ID="Question" runat="server" Width="200">
                                                <asp:ListItem Text="[Select a Question]" />
                                                <asp:ListItem Value="Favorite Pet" Text="Favorite Pet" />
                                                <asp:ListItem Value="Childhood Hero" Text="Childhood Hero"  />
                                                <asp:ListItem Value="Favorite Pastime" Text="Favorite Pastime"  />
                                                <asp:ListItem Value="Favorite Singer" Text="Favorite Singer" />
                                                <asp:ListItem Value="Favorite Band" Text="Favorite Band" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" InitialValue="[Select a Question]" ControlToValidate="Question"
                                                ErrorMessage="Select a Security Question" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator></p>
                                                
                                        <p><asp:Label CssClass="top" ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Your Answer:</asp:Label><br />
                                        <asp:TextBox runat="server" id="Answer" tabindex="5" CssClass="field"/><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ValidationGroup="CreateUserWizard1" ToolTip="Please enter an answer to help recover your password if lost" ControlToValidate="Answer">*</asp:RequiredFieldValidator><p />
                                    </fieldset>
                                 </div>
                                    
                                <br />    
                                <br />
                                <iZoom:SectionHeader ID="SectionHeader5" runat="server" Text="Verifcation" />
                                <div>
                                        <p><iZoom:CaptchaControl Font-Size="10px" TabIndex="6" TextboxCssClass="field" runat="server" ID="Captcha1" BorderStyle="None" BorderColor="#ffffff" CaptchaBackgroundNoise="medium" CaptchaFontWarping="Medium" CaptchaChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@&%" />&nbsp;
                                        <p><asp:CustomValidator ID="CaptchaValidator" runat="server" ValidationGroup="CreateUserWizard1" ErrorMessage="Please type characters from picture." ToolTip="Type characters from picture." OnServerValidate="CaptchaValidator_ServerValidate"></asp:CustomValidator>&nbsp;</p>
                                </div>

                                <asp:Label CssClass="error" ID="createUserErrorLabel" runat="server" />
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:CreateUserWizardStep>
                        
                        <asp:CompleteWizardStep runat="server">
                            <ContentTemplate>
                                <iZoom:PageTitle runat="server" ID="pageTitle" TitleText="Congratulations, Registration Complete!" DividerHeight="4" TitleSize="Largest" />
                                <p>You can use the navigation menu to the left for complete site navigation, or you may use the menu above for quick links to most popular links.</p>
                                <p><asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue"
                                                CssClass="button" Text="Continue" ValidationGroup="CreateUserWizard1" /></p>
                            </ContentTemplate>
                        </asp:CompleteWizardStep>
                    </wizardsteps>
            <stepstyle borderwidth="0px" />
        </asp:CreateUserWizard>
    </asp:Panel>
</asp:Content>




