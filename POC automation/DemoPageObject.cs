using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Automation
{
    class DemoPageObject
    {

        public string CheckboxCssSelector =
            ".checkboxDiv:nth-of-type({0})>div.form-group:nth-of-type({1}) .checkbox:nth-of-type({2}) input";
        public string foodList = "label.favfoodLabel~div label";

        public string elementsList = "div.form-group > label";

        public string fullName = "div .fullnameInput";

        public string gender = "div.radio input";

        public string email = "div .form-group input[type=email]";

        public string username = "label.usernameLabel + input";

        public string password = "label.passwordLabel + input";

        public string city = "label.cityLabel + input";

        public string state = "label.stateLabel + input";

        public string zipCode = "label.zipcodeLabel + input";

        public string contactNo = "label.contactLabel + input";

        public string introduction = "label.introLabel + textarea";

        public string hobbies = "label.hobbiesLabel + textarea";

        public string favFood = "div .checkbox input.foodOption";

        public string skills = "label.skillLabel + select";

        public string country = "div .form-group.col-lg-6.no-extra-padding:nth-child(2) label + select";

        public string submitBtn = "button.submitButton";

        public string resetBtn = "button.btn-info";
        public string fav = "div.form-group div.checkbox label input.favOption";

    }
}
