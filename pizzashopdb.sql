PGDMP                        }         	   Pizzashop    16.3    16.3 �    F           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            G           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            H           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            I           1262    24576 	   Pizzashop    DATABASE     ~   CREATE DATABASE "Pizzashop" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_India.1252';
    DROP DATABASE "Pizzashop";
                postgres    false            �           1247    24744    menu_item_type    TYPE     T   CREATE TYPE public.menu_item_type AS ENUM (
    'veg',
    'nonveg',
    'vegan'
);
 !   DROP TYPE public.menu_item_type;
       public          postgres    false            �           1247    24962 
   order_stat    TYPE     X   CREATE TYPE public.order_stat AS ENUM (
    'pending',
    'inprogress',
    'ready'
);
    DROP TYPE public.order_stat;
       public          postgres    false            �           1247    24957 	   order_typ    TYPE     F   CREATE TYPE public.order_typ AS ENUM (
    'dine in',
    'parcle'
);
    DROP TYPE public.order_typ;
       public          postgres    false            �           1247    25043 
   pay_method    TYPE     M   CREATE TYPE public.pay_method AS ENUM (
    'upi',
    'cash',
    'card'
);
    DROP TYPE public.pay_method;
       public          postgres    false            �           1247    24689 
   table_stat    TYPE     Y   CREATE TYPE public.table_stat AS ENUM (
    'occupied',
    'running',
    'reserved'
);
    DROP TYPE public.table_stat;
       public          postgres    false            �           1247    24837    tax_typ    TYPE     F   CREATE TYPE public.tax_typ AS ENUM (
    'percentage',
    'fixed'
);
    DROP TYPE public.tax_typ;
       public          postgres    false            �           1247    24738 	   unit_size    TYPE     @   CREATE TYPE public.unit_size AS ENUM (
    'g',
    'pieces'
);
    DROP TYPE public.unit_size;
       public          postgres    false            �            1259    24902 	   customers    TABLE     g  CREATE TABLE public.customers (
    customer_id integer NOT NULL,
    name character varying(20) NOT NULL,
    email text NOT NULL,
    phone_no character varying(15) NOT NULL,
    created_on timestamp without time zone DEFAULT CURRENT_DATE,
    created_by integer,
    update_by integer,
    updated_on date,
    is_deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false            �            1259    24901    customers_customer_id_seq    SEQUENCE     �   CREATE SEQUENCE public.customers_customer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.customers_customer_id_seq;
       public          postgres    false    242            J           0    0    customers_customer_id_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.customers_customer_id_seq OWNED BY public.customers.customer_id;
          public          postgres    false    241            �            1259    24885    item_modifier_group    TABLE       CREATE TABLE public.item_modifier_group (
    item_mod_id integer NOT NULL,
    item_id integer NOT NULL,
    modifer_id integer NOT NULL,
    minimun integer DEFAULT 1 NOT NULL,
    maximum integer DEFAULT 1 NOT NULL,
    isdeleted boolean DEFAULT false NOT NULL
);
 '   DROP TABLE public.item_modifier_group;
       public         heap    postgres    false            �            1259    24884 #   item_modifier_group_item_mod_id_seq    SEQUENCE     �   CREATE SEQUENCE public.item_modifier_group_item_mod_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 :   DROP SEQUENCE public.item_modifier_group_item_mod_id_seq;
       public          postgres    false    240            K           0    0 #   item_modifier_group_item_mod_id_seq    SEQUENCE OWNED BY     k   ALTER SEQUENCE public.item_modifier_group_item_mod_id_seq OWNED BY public.item_modifier_group.item_mod_id;
          public          postgres    false    239            �            1259    24720    menu_categories    TABLE     e  CREATE TABLE public.menu_categories (
    category_id integer NOT NULL,
    menu_name character varying(20) NOT NULL,
    menu_description character varying(255) NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer
);
 #   DROP TABLE public.menu_categories;
       public         heap    postgres    false            �            1259    24719    menu_categories_category_id_seq    SEQUENCE     �   CREATE SEQUENCE public.menu_categories_category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 6   DROP SEQUENCE public.menu_categories_category_id_seq;
       public          postgres    false    228            L           0    0    menu_categories_category_id_seq    SEQUENCE OWNED BY     c   ALTER SEQUENCE public.menu_categories_category_id_seq OWNED BY public.menu_categories.category_id;
          public          postgres    false    227            �            1259    24752 
   menu_items    TABLE     u  CREATE TABLE public.menu_items (
    iteam_id integer NOT NULL,
    category_id integer NOT NULL,
    iteam_name character varying(20) NOT NULL,
    item_type character varying(10) DEFAULT 'veg'::character varying NOT NULL,
    rate real DEFAULT 0 NOT NULL,
    quantity smallint DEFAULT 0 NOT NULL,
    unit character varying(10) DEFAULT 'g'::character varying NOT NULL,
    available boolean DEFAULT true,
    default_tax boolean DEFAULT false,
    tax_percentage smallint DEFAULT 0 NOT NULL,
    short_code smallint NOT NULL,
    description character varying(255) NOT NULL,
    iteam_img character varying(255),
    is_deleted boolean DEFAULT false NOT NULL,
    is_favourite boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer,
    CONSTRAINT check_item_type CHECK (((item_type)::text = ANY ((ARRAY['veg'::character varying, 'nonveg'::character varying, 'vegan'::character varying])::text[]))),
    CONSTRAINT check_unit CHECK (((unit)::text = ANY ((ARRAY['g'::character varying, 'pcs'::character varying])::text[])))
);
    DROP TABLE public.menu_items;
       public         heap    postgres    false            �            1259    24751    menu_items_iteam_id_seq    SEQUENCE     �   CREATE SEQUENCE public.menu_items_iteam_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.menu_items_iteam_id_seq;
       public          postgres    false    230            M           0    0    menu_items_iteam_id_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.menu_items_iteam_id_seq OWNED BY public.menu_items.iteam_id;
          public          postgres    false    229            �            1259    24820    modifier_group_mapping    TABLE     �   CREATE TABLE public.modifier_group_mapping (
    mapping_id integer NOT NULL,
    modifier_group_id integer NOT NULL,
    modifier_id integer NOT NULL,
    isdeleted boolean DEFAULT false NOT NULL
);
 *   DROP TABLE public.modifier_group_mapping;
       public         heap    postgres    false            �            1259    24819 %   modifier_group_mapping_mapping_id_seq    SEQUENCE     �   CREATE SEQUENCE public.modifier_group_mapping_mapping_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public.modifier_group_mapping_mapping_id_seq;
       public          postgres    false    236            N           0    0 %   modifier_group_mapping_mapping_id_seq    SEQUENCE OWNED BY     o   ALTER SEQUENCE public.modifier_group_mapping_mapping_id_seq OWNED BY public.modifier_group_mapping.mapping_id;
          public          postgres    false    235            �            1259    24782    modifier_groups    TABLE     k  CREATE TABLE public.modifier_groups (
    mod_group_id integer NOT NULL,
    modifier_name character varying(20) NOT NULL,
    group_description character varying(255) NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer
);
 #   DROP TABLE public.modifier_groups;
       public         heap    postgres    false            �            1259    24781     modifier_groups_mod_group_id_seq    SEQUENCE     �   CREATE SEQUENCE public.modifier_groups_mod_group_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 7   DROP SEQUENCE public.modifier_groups_mod_group_id_seq;
       public          postgres    false    232            O           0    0     modifier_groups_mod_group_id_seq    SEQUENCE OWNED BY     e   ALTER SEQUENCE public.modifier_groups_mod_group_id_seq OWNED BY public.modifier_groups.mod_group_id;
          public          postgres    false    231            �            1259    24800 	   modifiers    TABLE     �  CREATE TABLE public.modifiers (
    modifier_id integer NOT NULL,
    mod_name character varying(20) NOT NULL,
    rate real DEFAULT 0 NOT NULL,
    quantity smallint DEFAULT 0 NOT NULL,
    unit character varying(10) DEFAULT 'g'::character varying NOT NULL,
    modifier_desc character varying(255) NOT NULL,
    is_avail boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer,
    CONSTRAINT check_unit CHECK (((unit)::text = ANY ((ARRAY['g'::character varying, 'pcs'::character varying])::text[])))
);
    DROP TABLE public.modifiers;
       public         heap    postgres    false            �            1259    24799    modifiers_modifier_id_seq    SEQUENCE     �   CREATE SEQUENCE public.modifiers_modifier_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.modifiers_modifier_id_seq;
       public          postgres    false    234            P           0    0    modifiers_modifier_id_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.modifiers_modifier_id_seq OWNED BY public.modifiers.modifier_id;
          public          postgres    false    233            �            1259    24992    order_details    TABLE     w  CREATE TABLE public.order_details (
    order_details_id integer NOT NULL,
    order_id integer NOT NULL,
    item_id integer NOT NULL,
    quantity smallint NOT NULL,
    iteam_status character varying(10) DEFAULT 'pending'::character varying NOT NULL,
    instructions character varying(255),
    prepared smallint NOT NULL,
    CONSTRAINT check_iteam_status CHECK (((iteam_status)::text = ANY ((ARRAY['pending'::character varying, 'failed'::character varying, 'served'::character varying, 'completed'::character varying, 'cancelled'::character varying, 'onhold'::character varying, 'progress'::character varying])::text[])))
);
 !   DROP TABLE public.order_details;
       public         heap    postgres    false            �            1259    24991 "   order_details_order_details_id_seq    SEQUENCE     �   CREATE SEQUENCE public.order_details_order_details_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 9   DROP SEQUENCE public.order_details_order_details_id_seq;
       public          postgres    false    250            Q           0    0 "   order_details_order_details_id_seq    SEQUENCE OWNED BY     i   ALTER SEQUENCE public.order_details_order_details_id_seq OWNED BY public.order_details.order_details_id;
          public          postgres    false    249            �            1259    25009    order_item_modifier    TABLE     �   CREATE TABLE public.order_item_modifier (
    id integer NOT NULL,
    modifier_id integer NOT NULL,
    order_details_id integer NOT NULL
);
 '   DROP TABLE public.order_item_modifier;
       public         heap    postgres    false            �            1259    25008    order_item_modifier_id_seq    SEQUENCE     �   CREATE SEQUENCE public.order_item_modifier_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 1   DROP SEQUENCE public.order_item_modifier_id_seq;
       public          postgres    false    252            R           0    0    order_item_modifier_id_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public.order_item_modifier_id_seq OWNED BY public.order_item_modifier.id;
          public          postgres    false    251            �            1259    25026    order_table    TABLE     �   CREATE TABLE public.order_table (
    order_table_id integer NOT NULL,
    order_id integer NOT NULL,
    table_id integer NOT NULL
);
    DROP TABLE public.order_table;
       public         heap    postgres    false            �            1259    25025    order_table_order_table_id_seq    SEQUENCE     �   CREATE SEQUENCE public.order_table_order_table_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 5   DROP SEQUENCE public.order_table_order_table_id_seq;
       public          postgres    false    254            S           0    0    order_table_order_table_id_seq    SEQUENCE OWNED BY     a   ALTER SEQUENCE public.order_table_order_table_id_seq OWNED BY public.order_table.order_table_id;
          public          postgres    false    253                       1259    25070 	   order_tax    TABLE     �   CREATE TABLE public.order_tax (
    order_tax_id integer NOT NULL,
    order_id integer NOT NULL,
    tax_id integer NOT NULL,
    amount real NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.order_tax;
       public         heap    postgres    false                       1259    25069    order_tax_order_tax_id_seq    SEQUENCE     �   CREATE SEQUENCE public.order_tax_order_tax_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 1   DROP SEQUENCE public.order_tax_order_tax_id_seq;
       public          postgres    false    258            T           0    0    order_tax_order_tax_id_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public.order_tax_order_tax_id_seq OWNED BY public.order_tax.order_tax_id;
          public          postgres    false    257            �            1259    24970    orders    TABLE       CREATE TABLE public.orders (
    order_id integer NOT NULL,
    customer_id integer NOT NULL,
    order_type character varying(10) DEFAULT 'dinein'::character varying NOT NULL,
    order_status character varying(10) DEFAULT 'pending'::character varying NOT NULL,
    odr_comment character varying(255),
    total real DEFAULT 0 NOT NULL,
    is_deleted boolean NOT NULL,
    created_on timestamp without time zone DEFAULT CURRENT_DATE,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer,
    review_id integer,
    no_of_ppl integer NOT NULL,
    CONSTRAINT check_order_status CHECK (((order_status)::text = ANY ((ARRAY['pending'::character varying, 'failed'::character varying, 'served'::character varying, 'completed'::character varying, 'cancelled'::character varying, 'onhold'::character varying, 'progress'::character varying])::text[]))),
    CONSTRAINT check_order_type CHECK (((order_type)::text = ANY ((ARRAY['dinein'::character varying, 'parcle'::character varying])::text[])))
);
    DROP TABLE public.orders;
       public         heap    postgres    false            �            1259    24969    orders_order_id_seq    SEQUENCE     �   CREATE SEQUENCE public.orders_order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.orders_order_id_seq;
       public          postgres    false    248            U           0    0    orders_order_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.orders_order_id_seq OWNED BY public.orders.order_id;
          public          postgres    false    247                        1259    25050    payment    TABLE     �  CREATE TABLE public.payment (
    pay_id integer NOT NULL,
    customer_id integer NOT NULL,
    payment_method character varying(10) DEFAULT 'cash'::character varying NOT NULL,
    order_total real NOT NULL,
    payment_date timestamp without time zone NOT NULL,
    order_id integer NOT NULL,
    CONSTRAINT check_payment_method CHECK (((payment_method)::text = ANY ((ARRAY['upi'::character varying, 'cash'::character varying, 'card'::character varying])::text[])))
);
    DROP TABLE public.payment;
       public         heap    postgres    false            �            1259    25049    payment_pay_id_seq    SEQUENCE     �   CREATE SEQUENCE public.payment_pay_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.payment_pay_id_seq;
       public          postgres    false    256            V           0    0    payment_pay_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.payment_pay_id_seq OWNED BY public.payment.pay_id;
          public          postgres    false    255            �            1259    24634    permission_type    TABLE     �   CREATE TABLE public.permission_type (
    permisssion_type_id integer NOT NULL,
    permission_type character varying(20) NOT NULL
);
 #   DROP TABLE public.permission_type;
       public         heap    postgres    false            �            1259    24633 '   permission_type_permisssion_type_id_seq    SEQUENCE     �   CREATE SEQUENCE public.permission_type_permisssion_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 >   DROP SEQUENCE public.permission_type_permisssion_type_id_seq;
       public          postgres    false    220            W           0    0 '   permission_type_permisssion_type_id_seq    SEQUENCE OWNED BY     s   ALTER SEQUENCE public.permission_type_permisssion_type_id_seq OWNED BY public.permission_type.permisssion_type_id;
          public          postgres    false    219            �            1259    24641    permissions    TABLE     �  CREATE TABLE public.permissions (
    permission_id integer NOT NULL,
    role_id integer NOT NULL,
    permission_type_id integer NOT NULL,
    can_view boolean DEFAULT true NOT NULL,
    can_edit boolean DEFAULT true NOT NULL,
    can_delete boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer
);
    DROP TABLE public.permissions;
       public         heap    postgres    false            �            1259    24640    permissions_permission_id_seq    SEQUENCE     �   CREATE SEQUENCE public.permissions_permission_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public.permissions_permission_id_seq;
       public          postgres    false    222            X           0    0    permissions_permission_id_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE public.permissions_permission_id_seq OWNED BY public.permissions.permission_id;
          public          postgres    false    221                       1259    57906    reset_token    TABLE     �   CREATE TABLE public.reset_token (
    id integer NOT NULL,
    created_on date NOT NULL,
    valid_upto date NOT NULL,
    email text NOT NULL,
    token integer NOT NULL,
    used boolean DEFAULT false NOT NULL
);
    DROP TABLE public.reset_token;
       public         heap    postgres    false                       1259    57905    reset_token_id_seq    SEQUENCE     �   CREATE SEQUENCE public.reset_token_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.reset_token_id_seq;
       public          postgres    false    260            Y           0    0    reset_token_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.reset_token_id_seq OWNED BY public.reset_token.id;
          public          postgres    false    259            �            1259    24943    reviews    TABLE     �   CREATE TABLE public.reviews (
    id integer NOT NULL,
    customer_id integer NOT NULL,
    food smallint NOT NULL,
    service_ratings smallint NOT NULL,
    ambience smallint,
    descript character varying,
    order_id integer NOT NULL
);
    DROP TABLE public.reviews;
       public         heap    postgres    false            �            1259    24942    reviews_id_seq    SEQUENCE     �   CREATE SEQUENCE public.reviews_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.reviews_id_seq;
       public          postgres    false    246            Z           0    0    reviews_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.reviews_id_seq OWNED BY public.reviews.id;
          public          postgres    false    245            �            1259    24595    roles    TABLE     j   CREATE TABLE public.roles (
    role_id integer NOT NULL,
    role_name character varying(15) NOT NULL
);
    DROP TABLE public.roles;
       public         heap    postgres    false            �            1259    24594    roles_role_id_seq    SEQUENCE     �   CREATE SEQUENCE public.roles_role_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.roles_role_id_seq;
       public          postgres    false    216            [           0    0    roles_role_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.roles_role_id_seq OWNED BY public.roles.role_id;
          public          postgres    false    215            �            1259    24671    sections    TABLE     W  CREATE TABLE public.sections (
    section_id integer NOT NULL,
    sec_name character varying(15) NOT NULL,
    description character varying(255) NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer
);
    DROP TABLE public.sections;
       public         heap    postgres    false            �            1259    24670    sections_section_id_seq    SEQUENCE     �   CREATE SEQUENCE public.sections_section_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.sections_section_id_seq;
       public          postgres    false    224            \           0    0    sections_section_id_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.sections_section_id_seq OWNED BY public.sections.section_id;
          public          postgres    false    223            �            1259    24696    table_details    TABLE     �  CREATE TABLE public.table_details (
    table_id integer NOT NULL,
    tbl_name character varying(15) NOT NULL,
    section_id integer NOT NULL,
    capacity smallint DEFAULT 2 NOT NULL,
    table_status character varying(10) DEFAULT 'available'::character varying NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer,
    CONSTRAINT check_table_status CHECK (((table_status)::text = ANY ((ARRAY['available'::character varying, 'occupied'::character varying, 'running'::character varying])::text[])))
);
 !   DROP TABLE public.table_details;
       public         heap    postgres    false            �            1259    24695    table_details_table_id_seq    SEQUENCE     �   CREATE SEQUENCE public.table_details_table_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 1   DROP SEQUENCE public.table_details_table_id_seq;
       public          postgres    false    226            ]           0    0    table_details_table_id_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public.table_details_table_id_seq OWNED BY public.table_details.table_id;
          public          postgres    false    225            �            1259    24842    taxes    TABLE     �  CREATE TABLE public.taxes (
    tax_id integer NOT NULL,
    tax_name character varying(10) NOT NULL,
    tax_type character varying(10) DEFAULT 'percentage'::character varying NOT NULL,
    tax_amount real DEFAULT 0 NOT NULL,
    is_enabled boolean DEFAULT false NOT NULL,
    is_default boolean DEFAULT false NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_by integer,
    modified_by integer,
    created_at timestamp without time zone,
    modified_at timestamp without time zone,
    CONSTRAINT check_tax_type CHECK (((tax_type)::text = ANY ((ARRAY['percentage'::character varying, 'fixed'::character varying])::text[])))
);
    DROP TABLE public.taxes;
       public         heap    postgres    false            �            1259    24841    taxes_tax_id_seq    SEQUENCE     �   CREATE SEQUENCE public.taxes_tax_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.taxes_tax_id_seq;
       public          postgres    false    238            ^           0    0    taxes_tax_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.taxes_tax_id_seq OWNED BY public.taxes.tax_id;
          public          postgres    false    237            �            1259    24602    users    TABLE     R  CREATE TABLE public.users (
    user_id integer NOT NULL,
    fname character varying(15) NOT NULL,
    lname character varying(15),
    username character varying(20) NOT NULL,
    email text NOT NULL,
    password character varying(255) NOT NULL,
    profile_img character varying(255),
    country character varying(15) NOT NULL,
    state character varying(15) NOT NULL,
    city character varying(15) NOT NULL,
    zipcode character varying(10) NOT NULL,
    address character varying(255) NOT NULL,
    phone_no character varying(15) NOT NULL,
    status boolean DEFAULT false NOT NULL,
    role_id integer NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone DEFAULT CURRENT_DATE,
    updated_on timestamp without time zone DEFAULT CURRENT_DATE,
    created_by integer,
    updated_by integer
);
    DROP TABLE public.users;
       public         heap    postgres    false            �            1259    24601    users_user_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.users_user_id_seq;
       public          postgres    false    218            _           0    0    users_user_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;
          public          postgres    false    217            �            1259    24921 	   waitlists    TABLE     �  CREATE TABLE public.waitlists (
    token_id integer NOT NULL,
    no_people smallint NOT NULL,
    cust_name character varying(20) NOT NULL,
    cust_email character varying(20) NOT NULL,
    phone character varying(15) NOT NULL,
    sections_id integer NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_on timestamp without time zone DEFAULT CURRENT_DATE,
    updated_on timestamp without time zone,
    created_by integer,
    updated_by integer
);
    DROP TABLE public.waitlists;
       public         heap    postgres    false            �            1259    24920    waitlists_token_id_seq    SEQUENCE     �   CREATE SEQUENCE public.waitlists_token_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.waitlists_token_id_seq;
       public          postgres    false    244            `           0    0    waitlists_token_id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.waitlists_token_id_seq OWNED BY public.waitlists.token_id;
          public          postgres    false    243                       2604    57916    customers customer_id    DEFAULT     ~   ALTER TABLE ONLY public.customers ALTER COLUMN customer_id SET DEFAULT nextval('public.customers_customer_id_seq'::regclass);
 D   ALTER TABLE public.customers ALTER COLUMN customer_id DROP DEFAULT;
       public          postgres    false    242    241    242                        2604    57917    item_modifier_group item_mod_id    DEFAULT     �   ALTER TABLE ONLY public.item_modifier_group ALTER COLUMN item_mod_id SET DEFAULT nextval('public.item_modifier_group_item_mod_id_seq'::regclass);
 N   ALTER TABLE public.item_modifier_group ALTER COLUMN item_mod_id DROP DEFAULT;
       public          postgres    false    240    239    240            �           2604    57918    menu_categories category_id    DEFAULT     �   ALTER TABLE ONLY public.menu_categories ALTER COLUMN category_id SET DEFAULT nextval('public.menu_categories_category_id_seq'::regclass);
 J   ALTER TABLE public.menu_categories ALTER COLUMN category_id DROP DEFAULT;
       public          postgres    false    228    227    228            �           2604    57919    menu_items iteam_id    DEFAULT     z   ALTER TABLE ONLY public.menu_items ALTER COLUMN iteam_id SET DEFAULT nextval('public.menu_items_iteam_id_seq'::regclass);
 B   ALTER TABLE public.menu_items ALTER COLUMN iteam_id DROP DEFAULT;
       public          postgres    false    229    230    230            �           2604    57920 !   modifier_group_mapping mapping_id    DEFAULT     �   ALTER TABLE ONLY public.modifier_group_mapping ALTER COLUMN mapping_id SET DEFAULT nextval('public.modifier_group_mapping_mapping_id_seq'::regclass);
 P   ALTER TABLE public.modifier_group_mapping ALTER COLUMN mapping_id DROP DEFAULT;
       public          postgres    false    236    235    236            �           2604    57921    modifier_groups mod_group_id    DEFAULT     �   ALTER TABLE ONLY public.modifier_groups ALTER COLUMN mod_group_id SET DEFAULT nextval('public.modifier_groups_mod_group_id_seq'::regclass);
 K   ALTER TABLE public.modifier_groups ALTER COLUMN mod_group_id DROP DEFAULT;
       public          postgres    false    231    232    232            �           2604    57922    modifiers modifier_id    DEFAULT     ~   ALTER TABLE ONLY public.modifiers ALTER COLUMN modifier_id SET DEFAULT nextval('public.modifiers_modifier_id_seq'::regclass);
 D   ALTER TABLE public.modifiers ALTER COLUMN modifier_id DROP DEFAULT;
       public          postgres    false    233    234    234                       2604    57923    order_details order_details_id    DEFAULT     �   ALTER TABLE ONLY public.order_details ALTER COLUMN order_details_id SET DEFAULT nextval('public.order_details_order_details_id_seq'::regclass);
 M   ALTER TABLE public.order_details ALTER COLUMN order_details_id DROP DEFAULT;
       public          postgres    false    250    249    250                       2604    57924    order_item_modifier id    DEFAULT     �   ALTER TABLE ONLY public.order_item_modifier ALTER COLUMN id SET DEFAULT nextval('public.order_item_modifier_id_seq'::regclass);
 E   ALTER TABLE public.order_item_modifier ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    251    252    252                       2604    57925    order_table order_table_id    DEFAULT     �   ALTER TABLE ONLY public.order_table ALTER COLUMN order_table_id SET DEFAULT nextval('public.order_table_order_table_id_seq'::regclass);
 I   ALTER TABLE public.order_table ALTER COLUMN order_table_id DROP DEFAULT;
       public          postgres    false    254    253    254                       2604    57926    order_tax order_tax_id    DEFAULT     �   ALTER TABLE ONLY public.order_tax ALTER COLUMN order_tax_id SET DEFAULT nextval('public.order_tax_order_tax_id_seq'::regclass);
 E   ALTER TABLE public.order_tax ALTER COLUMN order_tax_id DROP DEFAULT;
       public          postgres    false    257    258    258                       2604    57927    orders order_id    DEFAULT     r   ALTER TABLE ONLY public.orders ALTER COLUMN order_id SET DEFAULT nextval('public.orders_order_id_seq'::regclass);
 >   ALTER TABLE public.orders ALTER COLUMN order_id DROP DEFAULT;
       public          postgres    false    248    247    248                       2604    57928    payment pay_id    DEFAULT     p   ALTER TABLE ONLY public.payment ALTER COLUMN pay_id SET DEFAULT nextval('public.payment_pay_id_seq'::regclass);
 =   ALTER TABLE public.payment ALTER COLUMN pay_id DROP DEFAULT;
       public          postgres    false    256    255    256            �           2604    57929 #   permission_type permisssion_type_id    DEFAULT     �   ALTER TABLE ONLY public.permission_type ALTER COLUMN permisssion_type_id SET DEFAULT nextval('public.permission_type_permisssion_type_id_seq'::regclass);
 R   ALTER TABLE public.permission_type ALTER COLUMN permisssion_type_id DROP DEFAULT;
       public          postgres    false    220    219    220            �           2604    57930    permissions permission_id    DEFAULT     �   ALTER TABLE ONLY public.permissions ALTER COLUMN permission_id SET DEFAULT nextval('public.permissions_permission_id_seq'::regclass);
 H   ALTER TABLE public.permissions ALTER COLUMN permission_id DROP DEFAULT;
       public          postgres    false    221    222    222                       2604    57909    reset_token id    DEFAULT     p   ALTER TABLE ONLY public.reset_token ALTER COLUMN id SET DEFAULT nextval('public.reset_token_id_seq'::regclass);
 =   ALTER TABLE public.reset_token ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    259    260    260            
           2604    57931 
   reviews id    DEFAULT     h   ALTER TABLE ONLY public.reviews ALTER COLUMN id SET DEFAULT nextval('public.reviews_id_seq'::regclass);
 9   ALTER TABLE public.reviews ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    246    245    246            �           2604    57932    roles role_id    DEFAULT     n   ALTER TABLE ONLY public.roles ALTER COLUMN role_id SET DEFAULT nextval('public.roles_role_id_seq'::regclass);
 <   ALTER TABLE public.roles ALTER COLUMN role_id DROP DEFAULT;
       public          postgres    false    216    215    216            �           2604    57933    sections section_id    DEFAULT     z   ALTER TABLE ONLY public.sections ALTER COLUMN section_id SET DEFAULT nextval('public.sections_section_id_seq'::regclass);
 B   ALTER TABLE public.sections ALTER COLUMN section_id DROP DEFAULT;
       public          postgres    false    224    223    224            �           2604    57934    table_details table_id    DEFAULT     �   ALTER TABLE ONLY public.table_details ALTER COLUMN table_id SET DEFAULT nextval('public.table_details_table_id_seq'::regclass);
 E   ALTER TABLE public.table_details ALTER COLUMN table_id DROP DEFAULT;
       public          postgres    false    225    226    226            �           2604    57935    taxes tax_id    DEFAULT     l   ALTER TABLE ONLY public.taxes ALTER COLUMN tax_id SET DEFAULT nextval('public.taxes_tax_id_seq'::regclass);
 ;   ALTER TABLE public.taxes ALTER COLUMN tax_id DROP DEFAULT;
       public          postgres    false    238    237    238            �           2604    57936    users user_id    DEFAULT     n   ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);
 <   ALTER TABLE public.users ALTER COLUMN user_id DROP DEFAULT;
       public          postgres    false    218    217    218                       2604    57937    waitlists token_id    DEFAULT     x   ALTER TABLE ONLY public.waitlists ALTER COLUMN token_id SET DEFAULT nextval('public.waitlists_token_id_seq'::regclass);
 A   ALTER TABLE public.waitlists ALTER COLUMN token_id DROP DEFAULT;
       public          postgres    false    243    244    244            1          0    24902 	   customers 
   TABLE DATA           �   COPY public.customers (customer_id, name, email, phone_no, created_on, created_by, update_by, updated_on, is_deleted) FROM stdin;
    public          postgres    false    242   y3      /          0    24885    item_modifier_group 
   TABLE DATA           l   COPY public.item_modifier_group (item_mod_id, item_id, modifer_id, minimun, maximum, isdeleted) FROM stdin;
    public          postgres    false    240   4      #          0    24720    menu_categories 
   TABLE DATA           �   COPY public.menu_categories (category_id, menu_name, menu_description, is_deleted, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    228   Y4      %          0    24752 
   menu_items 
   TABLE DATA           �   COPY public.menu_items (iteam_id, category_id, iteam_name, item_type, rate, quantity, unit, available, default_tax, tax_percentage, short_code, description, iteam_img, is_deleted, is_favourite, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    230   5      +          0    24820    modifier_group_mapping 
   TABLE DATA           g   COPY public.modifier_group_mapping (mapping_id, modifier_group_id, modifier_id, isdeleted) FROM stdin;
    public          postgres    false    236   J7      '          0    24782    modifier_groups 
   TABLE DATA           �   COPY public.modifier_groups (mod_group_id, modifier_name, group_description, is_deleted, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    232   �7      )          0    24800 	   modifiers 
   TABLE DATA           �   COPY public.modifiers (modifier_id, mod_name, rate, quantity, unit, modifier_desc, is_avail, is_deleted, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    234   �7      9          0    24992    order_details 
   TABLE DATA           |   COPY public.order_details (order_details_id, order_id, item_id, quantity, iteam_status, instructions, prepared) FROM stdin;
    public          postgres    false    250   W8      ;          0    25009    order_item_modifier 
   TABLE DATA           P   COPY public.order_item_modifier (id, modifier_id, order_details_id) FROM stdin;
    public          postgres    false    252   �8      =          0    25026    order_table 
   TABLE DATA           I   COPY public.order_table (order_table_id, order_id, table_id) FROM stdin;
    public          postgres    false    254   �8      A          0    25070 	   order_tax 
   TABLE DATA           W   COPY public.order_tax (order_tax_id, order_id, tax_id, amount, is_deleted) FROM stdin;
    public          postgres    false    258   "9      7          0    24970    orders 
   TABLE DATA           �   COPY public.orders (order_id, customer_id, order_type, order_status, odr_comment, total, is_deleted, created_on, updated_on, created_by, updated_by, review_id, no_of_ppl) FROM stdin;
    public          postgres    false    248   V9      ?          0    25050    payment 
   TABLE DATA           k   COPY public.payment (pay_id, customer_id, payment_method, order_total, payment_date, order_id) FROM stdin;
    public          postgres    false    256   �9                0    24634    permission_type 
   TABLE DATA           O   COPY public.permission_type (permisssion_type_id, permission_type) FROM stdin;
    public          postgres    false    220   
:                0    24641    permissions 
   TABLE DATA           �   COPY public.permissions (permission_id, role_id, permission_type_id, can_view, can_edit, can_delete, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    222   m:      C          0    57906    reset_token 
   TABLE DATA           U   COPY public.reset_token (id, created_on, valid_upto, email, token, used) FROM stdin;
    public          postgres    false    260   �:      5          0    24943    reviews 
   TABLE DATA           g   COPY public.reviews (id, customer_id, food, service_ratings, ambience, descript, order_id) FROM stdin;
    public          postgres    false    246   ;                0    24595    roles 
   TABLE DATA           3   COPY public.roles (role_id, role_name) FROM stdin;
    public          postgres    false    216   9;                0    24671    sections 
   TABLE DATA           �   COPY public.sections (section_id, sec_name, description, is_deleted, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    224   o;      !          0    24696    table_details 
   TABLE DATA           �   COPY public.table_details (table_id, tbl_name, section_id, capacity, table_status, is_deleted, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    226   �;      -          0    24842    taxes 
   TABLE DATA           �   COPY public.taxes (tax_id, tax_name, tax_type, tax_amount, is_enabled, is_default, is_deleted, created_by, modified_by, created_at, modified_at) FROM stdin;
    public          postgres    false    238   V<                0    24602    users 
   TABLE DATA           �   COPY public.users (user_id, fname, lname, username, email, password, profile_img, country, state, city, zipcode, address, phone_no, status, role_id, is_deleted, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    218   �<      3          0    24921 	   waitlists 
   TABLE DATA           �   COPY public.waitlists (token_id, no_people, cust_name, cust_email, phone, sections_id, is_deleted, created_on, updated_on, created_by, updated_by) FROM stdin;
    public          postgres    false    244   D      a           0    0    customers_customer_id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.customers_customer_id_seq', 24, true);
          public          postgres    false    241            b           0    0 #   item_modifier_group_item_mod_id_seq    SEQUENCE SET     R   SELECT pg_catalog.setval('public.item_modifier_group_item_mod_id_seq', 56, true);
          public          postgres    false    239            c           0    0    menu_categories_category_id_seq    SEQUENCE SET     N   SELECT pg_catalog.setval('public.menu_categories_category_id_seq', 30, true);
          public          postgres    false    227            d           0    0    menu_items_iteam_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.menu_items_iteam_id_seq', 49, true);
          public          postgres    false    229            e           0    0 %   modifier_group_mapping_mapping_id_seq    SEQUENCE SET     U   SELECT pg_catalog.setval('public.modifier_group_mapping_mapping_id_seq', 111, true);
          public          postgres    false    235            f           0    0     modifier_groups_mod_group_id_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.modifier_groups_mod_group_id_seq', 139, true);
          public          postgres    false    231            g           0    0    modifiers_modifier_id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.modifiers_modifier_id_seq', 20, true);
          public          postgres    false    233            h           0    0 "   order_details_order_details_id_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public.order_details_order_details_id_seq', 21, true);
          public          postgres    false    249            i           0    0    order_item_modifier_id_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.order_item_modifier_id_seq', 24, true);
          public          postgres    false    251            j           0    0    order_table_order_table_id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public.order_table_order_table_id_seq', 80, true);
          public          postgres    false    253            k           0    0    order_tax_order_tax_id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.order_tax_order_tax_id_seq', 8, true);
          public          postgres    false    257            l           0    0    orders_order_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.orders_order_id_seq', 72, true);
          public          postgres    false    247            m           0    0    payment_pay_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.payment_pay_id_seq', 7, true);
          public          postgres    false    255            n           0    0 '   permission_type_permisssion_type_id_seq    SEQUENCE SET     U   SELECT pg_catalog.setval('public.permission_type_permisssion_type_id_seq', 7, true);
          public          postgres    false    219            o           0    0    permissions_permission_id_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public.permissions_permission_id_seq', 22, true);
          public          postgres    false    221            p           0    0    reset_token_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.reset_token_id_seq', 6, true);
          public          postgres    false    259            q           0    0    reviews_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.reviews_id_seq', 4, true);
          public          postgres    false    245            r           0    0    roles_role_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.roles_role_id_seq', 3, true);
          public          postgres    false    215            s           0    0    sections_section_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.sections_section_id_seq', 21, true);
          public          postgres    false    223            t           0    0    table_details_table_id_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.table_details_table_id_seq', 32, true);
          public          postgres    false    225            u           0    0    taxes_tax_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.taxes_tax_id_seq', 12, true);
          public          postgres    false    237            v           0    0    users_user_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.users_user_id_seq', 42, true);
          public          postgres    false    217            w           0    0    waitlists_token_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.waitlists_token_id_seq', 45, true);
          public          postgres    false    243            D           2606    24909    customers customers_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (customer_id);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    242            B           2606    24890 ,   item_modifier_group item_modifier_group_pkey 
   CONSTRAINT     s   ALTER TABLE ONLY public.item_modifier_group
    ADD CONSTRAINT item_modifier_group_pkey PRIMARY KEY (item_mod_id);
 V   ALTER TABLE ONLY public.item_modifier_group DROP CONSTRAINT item_modifier_group_pkey;
       public            postgres    false    240            6           2606    24726 $   menu_categories menu_categories_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.menu_categories
    ADD CONSTRAINT menu_categories_pkey PRIMARY KEY (category_id);
 N   ALTER TABLE ONLY public.menu_categories DROP CONSTRAINT menu_categories_pkey;
       public            postgres    false    228            8           2606    24765    menu_items menu_items_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.menu_items
    ADD CONSTRAINT menu_items_pkey PRIMARY KEY (iteam_id);
 D   ALTER TABLE ONLY public.menu_items DROP CONSTRAINT menu_items_pkey;
       public            postgres    false    230            >           2606    24825 2   modifier_group_mapping modifier_group_mapping_pkey 
   CONSTRAINT     x   ALTER TABLE ONLY public.modifier_group_mapping
    ADD CONSTRAINT modifier_group_mapping_pkey PRIMARY KEY (mapping_id);
 \   ALTER TABLE ONLY public.modifier_group_mapping DROP CONSTRAINT modifier_group_mapping_pkey;
       public            postgres    false    236            :           2606    24788 $   modifier_groups modifier_groups_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.modifier_groups
    ADD CONSTRAINT modifier_groups_pkey PRIMARY KEY (mod_group_id);
 N   ALTER TABLE ONLY public.modifier_groups DROP CONSTRAINT modifier_groups_pkey;
       public            postgres    false    232            <           2606    24808    modifiers modifiers_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public.modifiers
    ADD CONSTRAINT modifiers_pkey PRIMARY KEY (modifier_id);
 B   ALTER TABLE ONLY public.modifiers DROP CONSTRAINT modifiers_pkey;
       public            postgres    false    234            L           2606    24997     order_details order_details_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.order_details
    ADD CONSTRAINT order_details_pkey PRIMARY KEY (order_details_id);
 J   ALTER TABLE ONLY public.order_details DROP CONSTRAINT order_details_pkey;
       public            postgres    false    250            N           2606    25014 ,   order_item_modifier order_item_modifier_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public.order_item_modifier
    ADD CONSTRAINT order_item_modifier_pkey PRIMARY KEY (id);
 V   ALTER TABLE ONLY public.order_item_modifier DROP CONSTRAINT order_item_modifier_pkey;
       public            postgres    false    252            P           2606    25031    order_table order_table_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.order_table
    ADD CONSTRAINT order_table_pkey PRIMARY KEY (order_table_id);
 F   ALTER TABLE ONLY public.order_table DROP CONSTRAINT order_table_pkey;
       public            postgres    false    254            T           2606    25075    order_tax order_tax_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.order_tax
    ADD CONSTRAINT order_tax_pkey PRIMARY KEY (order_tax_id);
 B   ALTER TABLE ONLY public.order_tax DROP CONSTRAINT order_tax_pkey;
       public            postgres    false    258            J           2606    24975    orders orders_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (order_id);
 <   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pkey;
       public            postgres    false    248            R           2606    25057    payment payment_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.payment
    ADD CONSTRAINT payment_pkey PRIMARY KEY (pay_id);
 >   ALTER TABLE ONLY public.payment DROP CONSTRAINT payment_pkey;
       public            postgres    false    256            .           2606    24639 $   permission_type permission_type_pkey 
   CONSTRAINT     s   ALTER TABLE ONLY public.permission_type
    ADD CONSTRAINT permission_type_pkey PRIMARY KEY (permisssion_type_id);
 N   ALTER TABLE ONLY public.permission_type DROP CONSTRAINT permission_type_pkey;
       public            postgres    false    220            0           2606    24649    permissions permissions_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.permissions
    ADD CONSTRAINT permissions_pkey PRIMARY KEY (permission_id);
 F   ALTER TABLE ONLY public.permissions DROP CONSTRAINT permissions_pkey;
       public            postgres    false    222            V           2606    57911    reset_token reset_token_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.reset_token
    ADD CONSTRAINT reset_token_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.reset_token DROP CONSTRAINT reset_token_pkey;
       public            postgres    false    260            H           2606    24950    reviews reviews_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT reviews_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.reviews DROP CONSTRAINT reviews_pkey;
       public            postgres    false    246            $           2606    24600    roles roles_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (role_id);
 :   ALTER TABLE ONLY public.roles DROP CONSTRAINT roles_pkey;
       public            postgres    false    216            2           2606    24677    sections sections_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.sections
    ADD CONSTRAINT sections_pkey PRIMARY KEY (section_id);
 @   ALTER TABLE ONLY public.sections DROP CONSTRAINT sections_pkey;
       public            postgres    false    224            4           2606    24703     table_details table_details_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.table_details
    ADD CONSTRAINT table_details_pkey PRIMARY KEY (table_id);
 J   ALTER TABLE ONLY public.table_details DROP CONSTRAINT table_details_pkey;
       public            postgres    false    226            @           2606    24851    taxes taxes_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.taxes
    ADD CONSTRAINT taxes_pkey PRIMARY KEY (tax_id);
 :   ALTER TABLE ONLY public.taxes DROP CONSTRAINT taxes_pkey;
       public            postgres    false    238            &           2606    24615    users users_email_key 
   CONSTRAINT     Q   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);
 ?   ALTER TABLE ONLY public.users DROP CONSTRAINT users_email_key;
       public            postgres    false    218            (           2606    24617    users users_phone_no_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_phone_no_key UNIQUE (phone_no);
 B   ALTER TABLE ONLY public.users DROP CONSTRAINT users_phone_no_key;
       public            postgres    false    218            *           2606    24611    users users_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    218            ,           2606    24613    users users_username_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);
 B   ALTER TABLE ONLY public.users DROP CONSTRAINT users_username_key;
       public            postgres    false    218            F           2606    24926    waitlists waitlists_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.waitlists
    ADD CONSTRAINT waitlists_pkey PRIMARY KEY (token_id);
 B   ALTER TABLE ONLY public.waitlists DROP CONSTRAINT waitlists_pkey;
       public            postgres    false    244            r           2606    24910 #   customers customers_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 M   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_created_by_fkey;
       public          postgres    false    218    242    4906            s           2606    24915 "   customers customers_update_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_update_by_fkey FOREIGN KEY (update_by) REFERENCES public.users(user_id);
 L   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_update_by_fkey;
       public          postgres    false    218    242    4906            p           2606    24891 4   item_modifier_group item_modifier_group_item_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.item_modifier_group
    ADD CONSTRAINT item_modifier_group_item_id_fkey FOREIGN KEY (item_id) REFERENCES public.menu_items(iteam_id);
 ^   ALTER TABLE ONLY public.item_modifier_group DROP CONSTRAINT item_modifier_group_item_id_fkey;
       public          postgres    false    230    4920    240            q           2606    25179 7   item_modifier_group item_modifier_group_modifer_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.item_modifier_group
    ADD CONSTRAINT item_modifier_group_modifer_id_fkey FOREIGN KEY (modifer_id) REFERENCES public.modifier_groups(mod_group_id);
 a   ALTER TABLE ONLY public.item_modifier_group DROP CONSTRAINT item_modifier_group_modifer_id_fkey;
       public          postgres    false    240    232    4922            c           2606    24727 /   menu_categories menu_categories_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.menu_categories
    ADD CONSTRAINT menu_categories_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 Y   ALTER TABLE ONLY public.menu_categories DROP CONSTRAINT menu_categories_created_by_fkey;
       public          postgres    false    228    218    4906            d           2606    24732 /   menu_categories menu_categories_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.menu_categories
    ADD CONSTRAINT menu_categories_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 Y   ALTER TABLE ONLY public.menu_categories DROP CONSTRAINT menu_categories_updated_by_fkey;
       public          postgres    false    218    4906    228            e           2606    24766 &   menu_items menu_items_category_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.menu_items
    ADD CONSTRAINT menu_items_category_id_fkey FOREIGN KEY (category_id) REFERENCES public.menu_categories(category_id);
 P   ALTER TABLE ONLY public.menu_items DROP CONSTRAINT menu_items_category_id_fkey;
       public          postgres    false    230    4918    228            f           2606    24771 %   menu_items menu_items_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.menu_items
    ADD CONSTRAINT menu_items_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 O   ALTER TABLE ONLY public.menu_items DROP CONSTRAINT menu_items_created_by_fkey;
       public          postgres    false    218    4906    230            g           2606    24776 %   menu_items menu_items_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.menu_items
    ADD CONSTRAINT menu_items_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 O   ALTER TABLE ONLY public.menu_items DROP CONSTRAINT menu_items_updated_by_fkey;
       public          postgres    false    4906    218    230            l           2606    24826 D   modifier_group_mapping modifier_group_mapping_modifier_group_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.modifier_group_mapping
    ADD CONSTRAINT modifier_group_mapping_modifier_group_id_fkey FOREIGN KEY (modifier_group_id) REFERENCES public.modifier_groups(mod_group_id);
 n   ALTER TABLE ONLY public.modifier_group_mapping DROP CONSTRAINT modifier_group_mapping_modifier_group_id_fkey;
       public          postgres    false    232    4922    236            m           2606    24831 >   modifier_group_mapping modifier_group_mapping_modifier_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.modifier_group_mapping
    ADD CONSTRAINT modifier_group_mapping_modifier_id_fkey FOREIGN KEY (modifier_id) REFERENCES public.modifiers(modifier_id);
 h   ALTER TABLE ONLY public.modifier_group_mapping DROP CONSTRAINT modifier_group_mapping_modifier_id_fkey;
       public          postgres    false    234    236    4924            h           2606    24789 /   modifier_groups modifier_groups_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.modifier_groups
    ADD CONSTRAINT modifier_groups_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 Y   ALTER TABLE ONLY public.modifier_groups DROP CONSTRAINT modifier_groups_created_by_fkey;
       public          postgres    false    218    232    4906            i           2606    24794 /   modifier_groups modifier_groups_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.modifier_groups
    ADD CONSTRAINT modifier_groups_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 Y   ALTER TABLE ONLY public.modifier_groups DROP CONSTRAINT modifier_groups_updated_by_fkey;
       public          postgres    false    218    232    4906            j           2606    24809 #   modifiers modifiers_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.modifiers
    ADD CONSTRAINT modifiers_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 M   ALTER TABLE ONLY public.modifiers DROP CONSTRAINT modifiers_created_by_fkey;
       public          postgres    false    218    234    4906            k           2606    24814 #   modifiers modifiers_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.modifiers
    ADD CONSTRAINT modifiers_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 M   ALTER TABLE ONLY public.modifiers DROP CONSTRAINT modifiers_updated_by_fkey;
       public          postgres    false    234    4906    218            }           2606    25003 (   order_details order_details_item_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_details
    ADD CONSTRAINT order_details_item_id_fkey FOREIGN KEY (item_id) REFERENCES public.menu_items(iteam_id);
 R   ALTER TABLE ONLY public.order_details DROP CONSTRAINT order_details_item_id_fkey;
       public          postgres    false    250    4920    230            ~           2606    24998 )   order_details order_details_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_details
    ADD CONSTRAINT order_details_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(order_id);
 S   ALTER TABLE ONLY public.order_details DROP CONSTRAINT order_details_order_id_fkey;
       public          postgres    false    250    248    4938                       2606    25015 8   order_item_modifier order_item_modifier_modifier_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_item_modifier
    ADD CONSTRAINT order_item_modifier_modifier_id_fkey FOREIGN KEY (modifier_id) REFERENCES public.modifiers(modifier_id);
 b   ALTER TABLE ONLY public.order_item_modifier DROP CONSTRAINT order_item_modifier_modifier_id_fkey;
       public          postgres    false    4924    234    252            �           2606    57899 8   order_item_modifier order_modifier_order_details_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_item_modifier
    ADD CONSTRAINT order_modifier_order_details_id_fkey FOREIGN KEY (order_details_id) REFERENCES public.order_details(order_details_id) NOT VALID;
 b   ALTER TABLE ONLY public.order_item_modifier DROP CONSTRAINT order_modifier_order_details_id_fkey;
       public          postgres    false    250    4940    252            �           2606    25032 %   order_table order_table_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_table
    ADD CONSTRAINT order_table_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(order_id);
 O   ALTER TABLE ONLY public.order_table DROP CONSTRAINT order_table_order_id_fkey;
       public          postgres    false    254    4938    248            �           2606    25037 %   order_table order_table_table_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_table
    ADD CONSTRAINT order_table_table_id_fkey FOREIGN KEY (table_id) REFERENCES public.table_details(table_id);
 O   ALTER TABLE ONLY public.order_table DROP CONSTRAINT order_table_table_id_fkey;
       public          postgres    false    4916    226    254            �           2606    25076 !   order_tax order_tax_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_tax
    ADD CONSTRAINT order_tax_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(order_id);
 K   ALTER TABLE ONLY public.order_tax DROP CONSTRAINT order_tax_order_id_fkey;
       public          postgres    false    258    4938    248            �           2606    25081    order_tax order_tax_tax_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_tax
    ADD CONSTRAINT order_tax_tax_id_fkey FOREIGN KEY (tax_id) REFERENCES public.taxes(tax_id);
 I   ALTER TABLE ONLY public.order_tax DROP CONSTRAINT order_tax_tax_id_fkey;
       public          postgres    false    4928    258    238            y           2606    24981    orders orders_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 G   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_created_by_fkey;
       public          postgres    false    218    248    4906            z           2606    24976    orders orders_customer_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(customer_id);
 H   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_customer_id_fkey;
       public          postgres    false    248    242    4932            {           2606    25169    orders orders_review_id_fkey    FK CONSTRAINT        ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_review_id_fkey FOREIGN KEY (review_id) REFERENCES public.reviews(id);
 F   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_review_id_fkey;
       public          postgres    false    246    248    4936            |           2606    24986    orders orders_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 G   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_updated_by_fkey;
       public          postgres    false    218    248    4906            �           2606    25063     payment payment_customer_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.payment
    ADD CONSTRAINT payment_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(customer_id);
 J   ALTER TABLE ONLY public.payment DROP CONSTRAINT payment_customer_id_fkey;
       public          postgres    false    4932    242    256            �           2606    25187    payment payment_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.payment
    ADD CONSTRAINT payment_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(order_id);
 G   ALTER TABLE ONLY public.payment DROP CONSTRAINT payment_order_id_fkey;
       public          postgres    false    248    256    4938            Z           2606    24660 '   permissions permissions_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.permissions
    ADD CONSTRAINT permissions_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 Q   ALTER TABLE ONLY public.permissions DROP CONSTRAINT permissions_created_by_fkey;
       public          postgres    false    4906    222    218            [           2606    24655 /   permissions permissions_permission_type_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.permissions
    ADD CONSTRAINT permissions_permission_type_id_fkey FOREIGN KEY (permission_type_id) REFERENCES public.permission_type(permisssion_type_id);
 Y   ALTER TABLE ONLY public.permissions DROP CONSTRAINT permissions_permission_type_id_fkey;
       public          postgres    false    4910    222    220            \           2606    24650 $   permissions permissions_role_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.permissions
    ADD CONSTRAINT permissions_role_id_fkey FOREIGN KEY (role_id) REFERENCES public.roles(role_id);
 N   ALTER TABLE ONLY public.permissions DROP CONSTRAINT permissions_role_id_fkey;
       public          postgres    false    4900    222    216            ]           2606    24665 '   permissions permissions_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.permissions
    ADD CONSTRAINT permissions_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 Q   ALTER TABLE ONLY public.permissions DROP CONSTRAINT permissions_updated_by_fkey;
       public          postgres    false    222    218    4906            w           2606    24951     reviews reviews_customer_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT reviews_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(customer_id);
 J   ALTER TABLE ONLY public.reviews DROP CONSTRAINT reviews_customer_id_fkey;
       public          postgres    false    4932    242    246            x           2606    57886    reviews reviews_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reviews
    ADD CONSTRAINT reviews_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(order_id) NOT VALID;
 G   ALTER TABLE ONLY public.reviews DROP CONSTRAINT reviews_order_id_fkey;
       public          postgres    false    4938    246    248            ^           2606    24678 !   sections sections_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.sections
    ADD CONSTRAINT sections_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 K   ALTER TABLE ONLY public.sections DROP CONSTRAINT sections_created_by_fkey;
       public          postgres    false    218    224    4906            _           2606    24683 !   sections sections_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.sections
    ADD CONSTRAINT sections_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 K   ALTER TABLE ONLY public.sections DROP CONSTRAINT sections_updated_by_fkey;
       public          postgres    false    224    4906    218            `           2606    24709 +   table_details table_details_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.table_details
    ADD CONSTRAINT table_details_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 U   ALTER TABLE ONLY public.table_details DROP CONSTRAINT table_details_created_by_fkey;
       public          postgres    false    218    4906    226            a           2606    24704 +   table_details table_details_section_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.table_details
    ADD CONSTRAINT table_details_section_id_fkey FOREIGN KEY (section_id) REFERENCES public.sections(section_id);
 U   ALTER TABLE ONLY public.table_details DROP CONSTRAINT table_details_section_id_fkey;
       public          postgres    false    224    226    4914            b           2606    24714 +   table_details table_details_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.table_details
    ADD CONSTRAINT table_details_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 U   ALTER TABLE ONLY public.table_details DROP CONSTRAINT table_details_updated_by_fkey;
       public          postgres    false    4906    226    218            n           2606    24852    taxes taxes_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.taxes
    ADD CONSTRAINT taxes_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 E   ALTER TABLE ONLY public.taxes DROP CONSTRAINT taxes_created_by_fkey;
       public          postgres    false    238    4906    218            o           2606    24857    taxes taxes_modified_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.taxes
    ADD CONSTRAINT taxes_modified_by_fkey FOREIGN KEY (modified_by) REFERENCES public.users(user_id);
 F   ALTER TABLE ONLY public.taxes DROP CONSTRAINT taxes_modified_by_fkey;
       public          postgres    false    238    218    4906            W           2606    24623    users users_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 E   ALTER TABLE ONLY public.users DROP CONSTRAINT users_created_by_fkey;
       public          postgres    false    218    4906    218            X           2606    24618    users users_role_id_fkey    FK CONSTRAINT     |   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_role_id_fkey FOREIGN KEY (role_id) REFERENCES public.roles(role_id);
 B   ALTER TABLE ONLY public.users DROP CONSTRAINT users_role_id_fkey;
       public          postgres    false    4900    218    216            Y           2606    24628    users users_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 E   ALTER TABLE ONLY public.users DROP CONSTRAINT users_updated_by_fkey;
       public          postgres    false    218    4906    218            t           2606    24932 #   waitlists waitlists_created_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.waitlists
    ADD CONSTRAINT waitlists_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(user_id);
 M   ALTER TABLE ONLY public.waitlists DROP CONSTRAINT waitlists_created_by_fkey;
       public          postgres    false    4906    244    218            u           2606    24927 $   waitlists waitlists_sections_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.waitlists
    ADD CONSTRAINT waitlists_sections_id_fkey FOREIGN KEY (sections_id) REFERENCES public.sections(section_id);
 N   ALTER TABLE ONLY public.waitlists DROP CONSTRAINT waitlists_sections_id_fkey;
       public          postgres    false    224    244    4914            v           2606    24937 #   waitlists waitlists_updated_by_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.waitlists
    ADD CONSTRAINT waitlists_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES public.users(user_id);
 M   ALTER TABLE ONLY public.waitlists DROP CONSTRAINT waitlists_updated_by_fkey;
       public          postgres    false    4906    244    218            1   x   x�32�t-K�+欨�rH�M���K��崴07136245�4202�50�54R04�26�2�г047����4.#N���<�Ĥd����A�F�&�f@��Z�����ZX ���� 6
#�      /   H   x�Mͱ�@C����s�]��N
:Y��^a8�Q��xw_�	�xz��R=�Rg��ĺ��)�f���y      #   �   x�]O�
�@<g��/(���U����=z	�TA!����W��B�����<�0��&�7wWBJH:.�
�{H�C�%�?��[dKlV�[�-�X��������Za�80�!#Cl���p�'��5���ǻF��U��(�z�/Gȱy�ZU��!���a       %   4  x���ˎ�0@��W�l$~�f9#��E�JM��d0���G����a&u+""����ڗ&�lr�6���e~� 
)�@��1�<7��2g`�����BE��T�=P�}_J�-c�W2����9�Ж{���K�'�\����� ������q������3ìU@C��He˱��J@d>���wû��}��1�"���z%܏��JT0O����&��bp
!�R�1��g��.�ntv�qA����ȩO��J��<�w�W��y���7�,��Ðp���������1<�m�(UN�ܑ�q�~M$��ǔ�83ML�M SpV���M��b&f�ǐ-�7t���2+E ����h&�b1a�蔲��ٱ�� �dE�hd�z5yn�żm3���<�9lyDÕcG�6'Ѥ�>�V�d֨R���W����o�ip�k�CLE��i�K˞�c�	l��s��r���\OB���mT�zpV�pJY�qc��B�\�q��,�iO��sKɨ7���Mcv֘�(Z�$0}��e��^iM�:o۽64�+$��#��f��^7S�      +   1   x�34��446�44�L�24�r� ���c	��9F�%\1z\\\ �	G      '   B   x�346�t����,�Qi�1~p�ehl������Z�K�TX ����B)����T���R4�b���� �>�      )   j   x�34��J�I,H���44�4�,H.F��p�q�����%�sFjJJb�)L1L ]���s~IIbz�BrFjjq*��)L�8�-���UU�E�99��b�4��qqq  �:�      9   S   x�]�;�0�z�0�v�w�!���W��7�E2��og? �&׆8i}5>a��*����ߛ]˨�Z\3�S��kU�.$o�S&�      ;   )   x�32�4��44�22�44�42�22��&`C�=... ~��      =      x�3��47�46�0�47�4������ *�Z      A   $   x�3�47��427�L� q8�����=... R��      7   ]   x�����0��)X �����)��&b�
h�萮��tY��1�c�\�m;��e5��D'I-X��V)�/��@ÿ	7.�$��>aꞈ.�� �      ?   7   x�3�42�LN,��4�4202�50�54R04�2��2�Գ4212��47����� �V�         S   x�3���Iu�K	H-��,.����2��M�+�2��/JI-�2�-R�Υ�%��@�	gHbRNjpjr	H�)�[�Z����� ��         �   x�m�K
�0םÈ/_��'p�x|a``�"��$�NԢ���q_�/�[dâ�8�+�Ӣ�ȻE'!��pҧ���y������ceAMZQ��uP��E�(�0v�����(��9����nNs��RJ/)j�      C      x������ � �      5      x������ � �         &   x�3�tL����2�t�HM�2��M�KLO-����� z��         J   x�3��t�,*.Qp���/Ba�q�����%�{Q~i^
T����Ȁ3859.��ASj�锘���Ө
b���� 
�/�      !   }   x��ϱ� �z��z�V�6�y��D�_���L�|���V�$j���ōˌ����:eJD3�J��C1y�b�H�Y�%�N[�-Z���Q��(�����sJ�%��u����߇*!�{�U�      -   =   x�34�t�,H-JN�+ILO�44�,�L�?8��tFWf�E��!g0v�JP���qqq �#x         b  x��W钢J�]��7lٗ���;n���aDG"�,*>��&ߋ���mOwܞ�[bR.��N�LhX�4�� I���V��Of����c�n��)g�N�.$H����ܼxx�:EL�\�J���:}�BG��jSZ.E(���w�z��X�;:�v30
-��_���C��{��k+�d($�Gio�j P A�\� ~Vǧk�?v�4��k㟬�j*_��N�j+����4��������B�p��;�ym��o�S�ӄ���p.�h�\�ku��0\�{k~���1��ϲ�g��G&��Q��dI�i�A+���7���nb5x�8�~!)��Fd���y�%��8FI��JB����fX��G�s���}�D�E�w�@�/�-u�Y��� ���;-�"V�uin�]O��"�P�xk��.=�&�βpBk5�_����P	�CH���\	x�߃�Q��v�(3�/e�U�K�#3�u�I�z��`���Y��%Y !>SM�%=0M�GL@�������^i��E8I�z��|5��2K�~���c+�d�8\���-`Zo�'�"���%+6����n	�8��q��}�����`�'�1H
_l�"6}��0|;�/��R3Jb@s��3$�c��ܠ��1L<��L5@�JZ�����^5�UT���U��f�dwd2�˴�(�<7!��ז�}/�S�`����蝛Z���ysf�[�%0�>�j�P�)�H�����Ѵ�������F�}�[�s���k~BŖ���$�\� ����k�vw=ַis��+m�\����:�2<�����H��i�s�N����jlg�h�{0�6%�a��y�f��H�$�* $�;n b)1�(MQ��ac��>�u�S'�������(�X���Ux5-����������9/�#�f�:��Px��=5�^1����\��b;���]���ة�������(�b}e�P��-�H���ܳ�|�u�$���y�w��g��g��:����с�ø����^�ݓZLR�4�3O�͋LG3~9�����b����ʅ�B�A9�п9N���B���p� ��n���Q.�Բ���]��G��D�ъ���3��-�}��jy���(��G.O*�AO�r�ʊc)E&]��7����n'���^ K�����A�`��Q���vt��W�Wőc����!v�,IJ��k�8+�J��p���ȥ�u�#mV"}��t��T�b9#-UB!���6]�W[7�'9�N�D�Bb���7"���� �H��膉����l��7L�49���m�/�e0O���1c�Ї�ڡ���s��2�~�	Cs�l���}V�Vi���d,id)� ��&mJ���(&>�C��Ɉ��p��0�^P仅����|b|�m�\��,��ęDE��{q�پ6�	*	����_Pu%ΰQ�I�!�2����������z�U>ʢ�n�jV0����*m7�ޏ�'<{��՛�z�+1'��F��\���?���������y^`YNd�;V)# #�Mt0�|?���*g����g�/6�)j�,��.s>/<�)��jPw6y��-��K;]�T�Y��,��#a󍄹��&d��#���������y\���~΀�����iYZ�f�`�	����XV����nQt��T�)�)Ai��KuȪ�I����PS&�ả4U��;��MVu���y����n�v
F���n��
`xk�]K�ϲ������\L��?���v���6���Q�>iAd[�֓gmڏLwc�����WR~nN&3���2�H%K:���Ѩ�bk{���<��1Jz��w(0ŏ�5�:0}eƇR����c���Ǐ���m      3   �   x�m�;��@и|
.�h��{>���!ؘ���`N�+-�J���� ����u�w�v�̲k�b��ɂ-ֹX�V���%k��xg�$̦)���=����B�-���	&p��/����ԜϘ/����WILނ�'K!�-f~���nQ�.�6�j������o��Y��cbq�?��)���IGN     