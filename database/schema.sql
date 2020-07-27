SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_with_oids = false;

----- I NEED HELP RESIDENT SUPPORT V3 -----
--
-- Name: i_need_help_resident_support_v3 id; Type: DEFAULT; Schema: public; Owner: postgres
--

CREATE TABLE public.i_need_help_resident_support_v3 (
    "id" int4 NOT NULL,
    "is_on_behalf" bool,
    "consent_to_complete_on_behalf" bool,
    "on_behalf_first_name" varchar ,
    "on_behalf_last_name" varchar,
    "on_behalf_email_address" varchar,
    "on_behalf_contact_number" varchar,
    "relationship_with_resident" varchar,
    "postcode" varchar,
    "uprn" varchar,
    "ward" varchar,
    "address_first_line" varchar,
    "address_second_line" varchar,
    "address_third_line" varchar,
    "getting_in_touch_reason" text,
    "help_with_accessing_food" bool,
    "help_with_accessing_medicine" bool,
    "help_with_accessing_other_essentials" bool,
    "help_with_debt_and_money" bool,
    "help_with_health" bool,
    "help_with_mental_health" bool,
    "help_with_accessing_internet" bool,
    "help_with_something_else" bool,
    "medicine_delivery_help_needed" bool,
    "is_pharmacist_able_to_deliver" bool,
    "when_is_medicines_delivered" varchar,
    "name_address_pharmacist" varchar,
    "urgent_essentials" varchar,
    "current_support" varchar,
    "current_support_feedback" text,
    "first_name" varchar,
    "last_name" varchar,
    "dob_month" varchar,
    "dob_year" varchar,
    "dob_day" varchar,
    "contact_telephone_number" varchar,
    "contact_mobile_number" varchar,
    "email_address" varchar,
    "gp_surgery_details" varchar,
    "number_of_children_under_18" varchar,
    "consent_to_share" bool,
    "date_time_recorded" timestamp,
    "urgent_essentials_anything_else" varchar,
    "help_with_housing" bool,
    "help_with_jobs_or_training" bool,
    "help_with_children_and_schools" bool,
    "help_with_disabilities" bool,
    "record_status" varchar,
    PRIMARY KEY ("id")
);

ALTER TABLE public.i_need_help_resident_support_v3 OWNER TO postgres;

-- Sequence and defined type
CREATE SEQUENCE public.i_need_help_resident_support_v3_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

--
-- Name: i_need_help_resident_support_v3 id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.i_need_help_resident_support_v3_id_seq OWNER TO postgres;

--
-- Name: i_need_help_resident_support_v3 id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.i_need_help_resident_support_v3_id_seq OWNED BY public.i_need_help_resident_support_v3.id;

--
-- Name: i_need_help_resident_support_v3 id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.i_need_help_resident_support_v3 ALTER COLUMN id SET DEFAULT nextval('public.i_need_help_resident_support_v3_id_seq'::regclass);
