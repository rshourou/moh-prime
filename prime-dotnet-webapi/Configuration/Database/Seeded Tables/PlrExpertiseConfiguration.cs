using System.Collections.Generic;
using Prime.Models.Plr;

namespace Prime.Configuration.Database
{
    public class PlrExpertiseConfiguration : SeededTable<PlrExpertise>
    {
        public override IEnumerable<PlrExpertise> SeedData
        {
            get
            {
                return new[] {
                    new PlrExpertise { Code = "MS",       Name = "Management Studies" },
                    new PlrExpertise { Code = "NON-SPEC", Name = "Non-Specialist" },
                    new PlrExpertise { Code = "NUCRX",    Name = "Nuclear Pharmacy" },
                    new PlrExpertise { Code = "NUTRX",    Name = "Nutrition Pharmacy" },
                    new PlrExpertise { Code = "ONCRX",    Name = "Oncology Pharmacy" },
                    new PlrExpertise { Code = "OP",       Name = "Oral Medicine and Oral Pathology" },
                    new PlrExpertise { Code = "ORD",      Name = "Oral and Maxillofacial Radiology" },
                    new PlrExpertise { Code = "ORTH",     Name = "Orthodontics" },
                    new PlrExpertise { Code = "OS",       Name = "Oral Maxillofacial Surgery" },
                    new PlrExpertise { Code = "PED",      Name = "Pediatric Dentistry" },
                    new PlrExpertise { Code = "PER",      Name = "Periodontics" },
                    new PlrExpertise { Code = "PHPHARM",  Name = "Pharmacotherapy" },
                    new PlrExpertise { Code = "PRO",      Name = "Prosthodontics" },
                    new PlrExpertise { Code = "PSYRX",    Name = "Psychiatric Pharmacy" },
                    new PlrExpertise { Code = "SMD1",     Name = "Addiction Medicine" },
                    new PlrExpertise { Code = "SMD10",    Name = "Certified Independent Medical Examiner" },
                    new PlrExpertise { Code = "SMD100",   Name = "Medical Disorders of Pregnancy" },
                    new PlrExpertise { Code = "LPNOR",    Name = "Operating Room" },
                    new PlrExpertise { Code = "SMD101",   Name = "Cardiovascular Disease" },
                    new PlrExpertise { Code = "SMD104",   Name = "Chemical, Biological, Radiological and Nuclear Warfare Medicine" },
                    new PlrExpertise { Code = "SMD11",    Name = "Chemical Dependency" },
                    new PlrExpertise { Code = "SMD12",    Name = "Child and Adolescent Psychiatry" },
                    new PlrExpertise { Code = "SMD13",    Name = "Chronic Pain Management" },
                    new PlrExpertise { Code = "SMD14",    Name = "Clinical Hypnosis" },
                    new PlrExpertise { Code = "SMD15",    Name = "Colon & Rectal Surgery" },
                    new PlrExpertise { Code = "SMD16",    Name = "Corneal & Extraocular Disease" },
                    new PlrExpertise { Code = "SMD17",    Name = "Counseling" },
                    new PlrExpertise { Code = "SMD18",    Name = "Critical Care Medicine" },
                    new PlrExpertise { Code = "SMD19",    Name = "Dermatology" },
                    new PlrExpertise { Code = "SMD2",     Name = "Adolescent Medicine" },
                    new PlrExpertise { Code = "CCFPEM",   Name = "Certificant; CFPC(E.M.)" },
                    new PlrExpertise { Code = "CMCE",     Name = "Community Medicine Certification" },
                    new PlrExpertise { Code = "CTI",      Name = "Computed Tomography Imaging" },
                    new PlrExpertise { Code = "DPH",      Name = "Dental Public Health" },
                    new PlrExpertise { Code = "DS",       Name = "Dental Sciences" },
                    new PlrExpertise { Code = "END",      Name = "Endodontics" },
                    new PlrExpertise { Code = "ERN1",     Name = "General Medicine" },
                    new PlrExpertise { Code = "ERN10",    Name = "Critical/ Intensive Care" },
                    new PlrExpertise { Code = "ERN11",    Name = "Occupational Health" },
                    new PlrExpertise { Code = "ERN12",    Name = "Home Care" },
                    new PlrExpertise { Code = "ERN13",    Name = "Geriatric / Long Term Care" },
                    new PlrExpertise { Code = "ERN14",    Name = "Community Health" },
                    new PlrExpertise { Code = "ERN15",    Name = "Ambulatory Care" },
                    new PlrExpertise { Code = "ERN16",    Name = "Nursing Administration" },
                    new PlrExpertise { Code = "ERN17",    Name = "Teaching" },
                    new PlrExpertise { Code = "ERN18",    Name = "Research" },
                    new PlrExpertise { Code = "ERN19",    Name = "Telehealth" },
                    new PlrExpertise { Code = "ERN2",     Name = "General Surgery" },
                    new PlrExpertise { Code = "CCFP",     Name = "Certificant; CFPC" },
                    new PlrExpertise { Code = "ERN3",     Name = "Pediatrics" },
                    new PlrExpertise { Code = "ERN4",     Name = "Maternal / Newborn" },
                    new PlrExpertise { Code = "ERN5",     Name = "Psychiatric/ Mental Health" },
                    new PlrExpertise { Code = "ERN6",     Name = "Oncology" },
                    new PlrExpertise { Code = "ERN7",     Name = "Rehabilitation" },
                    new PlrExpertise { Code = "ERN8",     Name = "Operating / Recovery Room" },
                    new PlrExpertise { Code = "ERN9",     Name = "Emergency Care/ Prehospital" },
                    new PlrExpertise { Code = "GPDEN",    Name = "General Practice" },
                    new PlrExpertise { Code = "GPMD",     Name = "General Practice" },
                    new PlrExpertise { Code = "GPPHARM",  Name = "General Practice" },
                    new PlrExpertise { Code = "LPNDIAL",  Name = "Dialysis" },
                    new PlrExpertise { Code = "LPNIMMUN", Name = "Immunization" },
                    new PlrExpertise { Code = "AMD35",    Name = "Nuclear Medicine - Full" },
                    new PlrExpertise { Code = "AMD36",    Name = "Sleep Interpreter - Respiratory Dis." },
                    new PlrExpertise { Code = "AMD37",    Name = "Magnetic Resonance - Limited" },
                    new PlrExpertise { Code = "AMD38",    Name = "Pulm Director - Level II" },
                    new PlrExpertise { Code = "AMD39",    Name = "Pulm Director - Level III" },
                    new PlrExpertise { Code = "AMD4",     Name = "Craniosacral Therapy(Complementary)" },
                    new PlrExpertise { Code = "AMD40",    Name = "Pulm Director - Level IV" },
                    new PlrExpertise { Code = "AMD41",    Name = "Sleep Director -Full" },
                    new PlrExpertise { Code = "AMD42",    Name = "Sleep Director - Pediatric Disorders" },
                    new PlrExpertise { Code = "AMD43",    Name = "Sleep Director - Respiratory Disorders" },
                    new PlrExpertise { Code = "AMD44",    Name = "Homeopathy (Complementary)" },
                    new PlrExpertise { Code = "AMD45",    Name = "Ultrasound - Obstetrical / Gynecology" },
                    new PlrExpertise { Code = "AMD46",    Name = "Echocardiography -Restricted" },
                    new PlrExpertise { Code = "AMD47",    Name = "Ultasound - Non-Specialist" },
                    new PlrExpertise { Code = "AMD48",    Name = "Nuclear Medicine - Restricted" },
                    new PlrExpertise { Code = "AMD49",    Name = "Addiction Medicine" },
                    new PlrExpertise { Code = "AMD5",     Name = "Doctor of Osteopathy" },
                    new PlrExpertise { Code = "AMD50",    Name = "Customized Nutritional Therapy(Comp.)" },
                    new PlrExpertise { Code = "AMD51",    Name = "Ultrasound -Abortion Transvaginal" },
                    new PlrExpertise { Code = "AMD52",    Name = "Ultrasound - Abortion Trans - abdominal" },
                    new PlrExpertise { Code = "AMD53",    Name = "Anti-Aging Medicine(Complementary)" },
                    new PlrExpertise { Code = "AMD54",    Name = "Primordial Sound Meditation(Complementary)" },
                    new PlrExpertise { Code = "AMD55",    Name = "Ayurveda(Complementary)" },
                    new PlrExpertise { Code = "AMD56",    Name = "Fluoroscopically-guided needle placements" },
                    new PlrExpertise { Code = "AMD57",    Name = "Magnetic Resonance - Cardiac" },
                    new PlrExpertise { Code = "AMD58",    Name = "PET -Positron Emission Tomography" },
                    new PlrExpertise { Code = "AMD6",     Name = "Electrocardiogram" },
                    new PlrExpertise { Code = "AMD7",     Name = "Electroencephalography" },
                    new PlrExpertise { Code = "AMD8",     Name = "Electromyography" },
                    new PlrExpertise { Code = "AMD9",     Name = "Evoked Potential Auditory" },
                    new PlrExpertise { Code = "BI",       Name = "Breast Imaging" },
                    new PlrExpertise { Code = "A225",     Name = "Max / Fax Surgery" },
                    new PlrExpertise { Code = " A240",    Name = "Medical Imaging" },
                    new PlrExpertise { Code = "A385",     Name = "Pulmonary Diseases" },
                    new PlrExpertise { Code = "AMD1",     Name = "Acupuncture" },
                    new PlrExpertise { Code = "AMD10",    Name = "Evoked Potential Somatosensory" },
                    new PlrExpertise { Code = "AMD11",    Name = "Evoked Potential Visual" },
                    new PlrExpertise { Code = "AMD12",    Name = "Evoked Potential(EP)" },
                    new PlrExpertise { Code = "AMD13",    Name = "Functional Medicine(Complementary)" },
                    new PlrExpertise { Code = "AMD14",    Name = "Herbal Therapy(Complementary)" },
                    new PlrExpertise { Code = "AMD15",    Name = "Kinesiology(Complementary)" },
                    new PlrExpertise { Code = "AMD16",    Name = "Magnetic Resonance - Full" },
                    new PlrExpertise { Code = "AMD17",    Name = "Cardiac Nuclear Imaging" },
                    new PlrExpertise { Code = "AMD18",    Name = "Nuclear Medicine - Limited" },
                    new PlrExpertise { Code = "AMD19",    Name = "Ultrasound - Opthalmology" },
                    new PlrExpertise { Code = "AMD2",     Name = "Echocardiography -Full Adult" },
                    new PlrExpertise { Code = "AMD20",    Name = "Echocardiography - Full Pediatric" },
                    new PlrExpertise { Code = "AMD21",    Name = "Pulm Interpretation-Level II" },
                    new PlrExpertise { Code = "AMD22",    Name = "Pulm Interpretation-Level III" },
                    new PlrExpertise { Code = "AMD23",    Name = "Pulm Interpretation-Level IV" },
                    new PlrExpertise { Code = "AMD24",    Name = "Reiki Therapy(Complementary)" },
                    new PlrExpertise { Code = "AMD25",    Name = "Thought Field Therapy(Complementary)" },
                    new PlrExpertise { Code = "AMD26",    Name = "Transcendental Meditation(Comp.)" },
                    new PlrExpertise { Code = "AMD27",    Name = "Echocardiography -Transesophageal" },
                    new PlrExpertise { Code = "AMD28",    Name = "Trigger Point Therapy" },
                    new PlrExpertise { Code = "AMD29",    Name = "Ultrasound -Full" },
                    new PlrExpertise { Code = "AMD3",     Name = "Cardiac Stress Testing" },
                    new PlrExpertise { Code = "AMD30",    Name = "Ultrasound -Urology" },
                    new PlrExpertise { Code = "AMD31",    Name = "Ultrasound -Vascular" },
                    new PlrExpertise { Code = "AMD32",    Name = "Vestibular Testing - Director" },
                    new PlrExpertise { Code = "AMD33",    Name = "Vestibular Testing - Interpretor" },
                    new PlrExpertise { Code = "AMD34",    Name = "Chelation Therapy(Complementary)" },
                    new PlrExpertise { Code = "480",      Name = "Thoracic Surgery(Gen.Sur.)" },
                    new PlrExpertise { Code = "481",      Name = "Thoracic Surgery(General / Cardiac)" },
                    new PlrExpertise { Code = "490",      Name = "Vascular Surgery" },
                    new PlrExpertise { Code = "510",      Name = "Clinical Pharmacology" },
                    new PlrExpertise { Code = "512",      Name = "Colorectal Surgery" },
                    new PlrExpertise { Code = "515",      Name = "Critical Care medicine" },
                    new PlrExpertise { Code = "520",      Name = "General Surgical Oncology" },
                    new PlrExpertise { Code = "530",      Name = "Gynecology Oncology" },
                    new PlrExpertise { Code = "535",      Name = "Gynecology Reprod. Endocrin. & Infert." },
                    new PlrExpertise { Code = "540",      Name = "Transfusion Medicine" },
                    new PlrExpertise { Code = "545",      Name = "Maternal - Fetal Medicine" },
                    new PlrExpertise { Code = "550",      Name = "Clinician Investigator Program" },
                    new PlrExpertise { Code = "555",      Name = "Neonatal - Perinatal Medicine" },
                    new PlrExpertise { Code = "560",      Name = "Neuroradiology" },
                    new PlrExpertise { Code = "570",      Name = "Palliative Medicine" },
                    new PlrExpertise { Code = "580",      Name = "Pediatric Radiology" },
                    new PlrExpertise { Code = "582",      Name = "Pediatric Emergency Medicine" },
                    new PlrExpertise { Code = "584",      Name = "Developmental Pediatrics" },
                    new PlrExpertise { Code = "474",      Name = "Rheumatology(Int.Med or Ped.)" },
                    new PlrExpertise { Code = "900",      Name = "Pediatric Gastroenterology" },
                    new PlrExpertise { Code = "905",      Name = "Pediatric Nephrology" },
                    new PlrExpertise { Code = "910",      Name = "Pediatric Neurology" },
                    new PlrExpertise { Code = "915",      Name = "Pediatric Cardiovascular Surgery" },
                    new PlrExpertise { Code = "A110",     Name = "Administrative Medicine" },
                    new PlrExpertise { Code = "A115",     Name = "Adolescent Medicine" },
                    new PlrExpertise { Code = "A120",     Name = "Allergy" },
                    new PlrExpertise { Code = "A130",     Name = "Aviation Medicine" },
                    new PlrExpertise { Code = "A135",     Name = "Behavioral Therapy" },
                    new PlrExpertise { Code = "A140",     Name = "Child Health" },
                    new PlrExpertise { Code = "A145",     Name = "Cytopathology" },
                    new PlrExpertise { Code = "A155",     Name = "Epidemiology" },
                    new PlrExpertise { Code = "A160",     Name = "Family Medicine" },
                    new PlrExpertise { Code = "315",      Name = "General Surgery" },
                    new PlrExpertise { Code = "319",      Name = "Gynecology" },
                    new PlrExpertise { Code = "320",      Name = "Obstetrics" },
                    new PlrExpertise { Code = "325",      Name = "Medical Scientist(Surgery)" },
                    new PlrExpertise { Code = "329",      Name = "Neurosurgery" },
                    new PlrExpertise { Code = "331",      Name = "No Specialty(Surgery)" },
                    new PlrExpertise { Code = "336",      Name = "Obstetrics and Gynecology" },
                    new PlrExpertise { Code = "338",      Name = "Ophthalmology" },
                    new PlrExpertise { Code = "340",      Name = "Orthopedic Surgery" },
                    new PlrExpertise { Code = "342",      Name = "Otolaryngology" },
                    new PlrExpertise { Code = "345",      Name = "Plastic Surgery" },
                    new PlrExpertise { Code = "348",      Name = "Principles of Surgery" },
                    new PlrExpertise { Code = "354",      Name = "Thoracic Surgery" },
                    new PlrExpertise { Code = "355",      Name = "Thoracic Surgery" },
                    new PlrExpertise { Code = "356",      Name = "Cardiac Surgery" },
                    new PlrExpertise { Code = "360",      Name = "Urology" },
                    new PlrExpertise { Code = "405",      Name = "Cardiology(Int.Med.or Ped.)" },
                    new PlrExpertise { Code = "409",      Name = "Clinical Imm. Allergy(Int.Med.or Ped.)" },
                    new PlrExpertise { Code = "305",      Name = "Cardiovascular and Thoracic Surgery" },
                    new PlrExpertise { Code = "411",      Name = "Critical Care Medicine" },
                    new PlrExpertise { Code = "415",      Name = "Endocrin.Metabolism(Int.Med.or Ped.)" },
                    new PlrExpertise { Code = "417",      Name = "Forensic Pathology(Anatomical/ General)" },
                    new PlrExpertise { Code = "420",      Name = "Gastroenterology(Int.Med.or Ped.)" },
                    new PlrExpertise { Code = "425",      Name = "Geriatric Medicine(Int.Med.)" },
                    new PlrExpertise { Code = "430",      Name = "Hematology(Int.Med or Ped.)" },
                    new PlrExpertise { Code = "431",      Name = "Pediatric Hemat./ Onc. (Pediatrics)" },
                    new PlrExpertise { Code = "432",      Name = "Neuropathology" },
                    new PlrExpertise { Code = "435",      Name = "Infectious Diseases(Int.Med or Ped.)" },
                    new PlrExpertise { Code = "452",      Name = "Medical Oncology(Int.Med.)" },
                    new PlrExpertise { Code = "460",      Name = "Nephrology(Int.Med or Ped.)" },
                    new PlrExpertise { Code = "465",      Name = "Pediatric General Surgery" },
                    new PlrExpertise { Code = "472",      Name = "Respirology(Int.Med.or Ped.)" },
                    new PlrExpertise { Code = "118",      Name = "Diagnostic and Therapeutic Radiology" },
                    new PlrExpertise { Code = "122",      Name = "Emergency Medicine" },
                    new PlrExpertise { Code = "126",      Name = "Gastroenterology" },
                    new PlrExpertise { Code = "130",      Name = "Hematology" },
                    new PlrExpertise { Code = "136",      Name = "Internal Medicine" },
                    new PlrExpertise { Code = "140",      Name = "Medical Scientist(Medicine)" },
                    new PlrExpertise { Code = "145",      Name = "Neurology" },
                    new PlrExpertise { Code = "146",      Name = "Neurology and/ or Psychiatry" },
                    new PlrExpertise { Code = "149",      Name = "No Specialty(Medicine)" },
                    new PlrExpertise { Code = "150",      Name = "Medical Genetics" },
                    new PlrExpertise { Code = "151",      Name = "Nuclear Medicine" },
                    new PlrExpertise { Code = "152",      Name = "Urological Oncology" },
                    new PlrExpertise { Code = "153",      Name = "Paediatric Oncology" },
                    new PlrExpertise { Code = "154",      Name = "Community Oncology" },
                    new PlrExpertise { Code = "155",      Name = "Occupational Medicine" },
                    new PlrExpertise { Code = "159",      Name = "Pediatric Cardiology" },
                    new PlrExpertise { Code = "160",      Name = "Pediatrics" },
                    new PlrExpertise { Code = "162",      Name = "Physical Medicine Rehabilitation" },
                    new PlrExpertise { Code = "165",      Name = "Psychiatry" },
                    new PlrExpertise { Code = "168",      Name = "Public Health" },
                    new PlrExpertise { Code = "172",      Name = "Radiation Oncology" },
                    new PlrExpertise { Code = "176",      Name = "Respirology" },
                    new PlrExpertise { Code = "180",      Name = "Rheumatology" },
                    new PlrExpertise { Code = "190",      Name = "Therapeutic Radiology" },
                    new PlrExpertise { Code = "202",      Name = "Anatomical Pathology" },
                    new PlrExpertise { Code = "210",      Name = "General Pathology" },
                    new PlrExpertise { Code = "215",      Name = "Hematological Pathology" },
                    new PlrExpertise { Code = "225",      Name = "Medical Biochemistry" },
                    new PlrExpertise { Code = "227",      Name = "Medical Microbiology" },
                    new PlrExpertise { Code = "232",      Name = "Neuropathology" },
                    new PlrExpertise { Code = "240",      Name = "Pathology and Bacteriology" },
                    new PlrExpertise { Code = "302",      Name = "Cardiothoracic Surgery" },
                    new PlrExpertise { Code = "15 ",      Name = "Malignant Disease Specialist" },
                    new PlrExpertise { Code = "101",      Name = "Anaesthesia" },
                    new PlrExpertise { Code = "105",      Name = "Cardiology" },
                    new PlrExpertise { Code = "107",      Name = "Clinical Immunology" },
                    new PlrExpertise { Code = "108",      Name = "Clinical Immunology and Allergy" },
                    new PlrExpertise { Code = "110",      Name = "Community Medicine" },
                    new PlrExpertise { Code = "115",      Name = "Dermatology" },
                    new PlrExpertise { Code = "117",      Name = "Diagnostic Radiology" },
                    new PlrExpertise { Code = "SMD77",    Name = "Pediatric Rheumatology" },
                    new PlrExpertise { Code = "SMD78",    Name = "Perinatology" },
                    new PlrExpertise { Code = "SMD79",    Name = "Phlebology" },
                    new PlrExpertise { Code = "SMD8",     Name = "Breastfeeding" },
                    new PlrExpertise { Code = "SMD80",    Name = "Phlebology / Sclerotherapy" },
                    new PlrExpertise { Code = "SMD81",    Name = "Psychiatry" },
                    new PlrExpertise { Code = "SMD82",    Name = "Psychological Disorders" },
                    new PlrExpertise { Code = "SMD83",    Name = "Psychotherapy" },
                    new PlrExpertise { Code = "SMD84",    Name = "Psychotherapy / Hypnotherapy" },
                    new PlrExpertise { Code = "SMD85",    Name = "Psychotherapy: Indiv.Fam. & Marital" },
                    new PlrExpertise { Code = "SMD86",    Name = "Reproductive Endocrinology" },
                    new PlrExpertise { Code = "SMD87",    Name = "Sclerotherapy of Varicose Veins" },
                    new PlrExpertise { Code = "SMD88",    Name = "Shoulder Surgery" },
                    new PlrExpertise { Code = "SMD89",    Name = "Sleep Disorders Medicine" },
                    new PlrExpertise { Code = "SMD9",     Name = "Cardiac Rhythm Disturbances Pacemaker" },
                    new PlrExpertise { Code = "SMD90",    Name = "Sport Medicine" },
                    new PlrExpertise { Code = "SMD91",    Name = "Travel Medicine" },
                    new PlrExpertise { Code = "SMD92",    Name = "Treatment of Stress Disorders" },
                    new PlrExpertise { Code = "SMD93",    Name = "Uro - oncology" },
                    new PlrExpertise { Code = "SMD94",    Name = "Urogynecology" },
                    new PlrExpertise { Code = "SMD95",    Name = "Non-Spine Musculoskeletal Conditions" },
                    new PlrExpertise { Code = "SMD96",    Name = "Burn Surgery" },
                    new PlrExpertise { Code = "SMD97",    Name = "Speech and Hearing Disorders" },
                    new PlrExpertise { Code = "SMD98",    Name = "Oncological Surgery Liver and Pancreas" },
                    new PlrExpertise { Code = "SMD99",    Name = "Child and Maternal Health" },
                    new PlrExpertise { Code = "SMD5",     Name = "Arthro.And Reconstruct. Joint Surgery" },
                    new PlrExpertise { Code = "SMD50",    Name = "Knee Surgery" },
                    new PlrExpertise { Code = "SMD51",    Name = "Laparoscopic General Surgery" },
                    new PlrExpertise { Code = "SMD52",    Name = "Laser Surgery" },
                    new PlrExpertise { Code = "SMD53",    Name = "Mammography" },
                    new PlrExpertise { Code = "SMD54",    Name = "Maternity Care & Diseases of Females" },
                    new PlrExpertise { Code = "SMD55",    Name = "Medical Genetics" },
                    new PlrExpertise { Code = "SMD56",    Name = "Neonatology" },
                    new PlrExpertise { Code = "SMD57",    Name = "Neuro - ophthalmology" },
                    new PlrExpertise { Code = "SMD58",    Name = "Neuro-otology" },
                    new PlrExpertise { Code = "SMD59",    Name = "Neuroanesthesia" },
                    new PlrExpertise { Code = "SMD6",     Name = "Arthro. And Recon. Surg. (Knee / Shoulder)" },
                    new PlrExpertise { Code = "SMD60",    Name = "Nutrition" },
                    new PlrExpertise { Code = "SMD61",    Name = "Obstetrics &Gynecology" },
                    new PlrExpertise { Code = "SMD62",    Name = "Occupation Related Medical Disorders" },
                    new PlrExpertise { Code = "SMD63",    Name = "Occupational & Aviation Medicine" },
                    new PlrExpertise { Code = "SMD64",    Name = "Occupational Health" },
                    new PlrExpertise { Code = "SMD65",    Name = "Occupational Medicine" },
                    new PlrExpertise { Code = "SMD49",    Name = "Knee & Leg Surgery" },
                    new PlrExpertise { Code = "SMD66",    Name = "Occupational Ortho. Clinical Impairment" },
                    new PlrExpertise { Code = "SMD67",    Name = "Oculo-Plastics" },
                    new PlrExpertise { Code = "SMD68",    Name = "Ophthalmology" },
                    new PlrExpertise { Code = "SMD69",    Name = "OrthopedicSports Medicine in Children" },
                    new PlrExpertise { Code = "SMD7",     Name = "Aviation Medicine" },
                    new PlrExpertise { Code = "SMD70",    Name = "Orthopedic Traumatology" },
                    new PlrExpertise { Code = "SMD71",    Name = "Pain Management" },
                    new PlrExpertise { Code = "SMD72",    Name = "Pediatric &Adolescent Gynecology" },
                    new PlrExpertise { Code = "SMD73",    Name = "Pediatric Ophthamology Adult Strabismus" },
                    new PlrExpertise { Code = "SMD74",    Name = "Pediatric Orthopedic Surgery" },
                    new PlrExpertise { Code = "SMD75",    Name = "Pediatric Orthopedics" },
                    new PlrExpertise { Code = "SMD76",    Name = "Pediatric Otolaryngology" },
                    new PlrExpertise { Code = "SMD21",    Name = "Diabetes" },
                    new PlrExpertise { Code = "SMD22",    Name = "Diseases of Children" },
                    new PlrExpertise { Code = "SMD23",    Name = "Diseases of the Colon and Rectum" },
                    new PlrExpertise { Code = "SMD24",    Name = "Diseases of the Eye" },
                    new PlrExpertise { Code = "SMD25",    Name = "Diseases of the Joints" },
                    new PlrExpertise { Code = "SMD26",    Name = "Diseases of the Kidney" },
                    new PlrExpertise { Code = "SMD27",    Name = "Resp. Tract Diseases -Children" },
                    new PlrExpertise { Code = "SMD28",    Name = "Echocardiography" },
                    new PlrExpertise { Code = "SMD29",    Name = "Emergency Care" },
                    new PlrExpertise { Code = "SMD3",     Name = "Allergies" },
                    new PlrExpertise { Code = "SMD30",    Name = "Endocrinology" },
                    new PlrExpertise { Code = "SMD31",    Name = "Endoscopic General Surgery" },
                    new PlrExpertise { Code = "SMD32",    Name = "Exercise Medicine" },
                    new PlrExpertise { Code = "SMD33",    Name = "Family Therapy" },
                    new PlrExpertise { Code = "SMD34",    Name = "Female Incontinence & Urodynamics" },
                    new PlrExpertise { Code = "SMD35",    Name = "Forensic Pathology" },
                    new PlrExpertise { Code = "SMD36",    Name = "Forensic Psychiatry" },
                    new PlrExpertise { Code = "SMD37",    Name = "Forensic Psychiatry Medico-legal Med." },
                    new PlrExpertise { Code = "SMD38",    Name = "Gastrointestinal Surgery" },
                    new PlrExpertise { Code = "SMD20",    Name = "Dermatopathology" },
                    new PlrExpertise { Code = "SMD39",    Name = "Geriatric Medicine" },
                    new PlrExpertise { Code = "SMD4",     Name = "Allergies -Upper Respiratory Tract" },
                    new PlrExpertise { Code = "SMD40",    Name = "Geriatric Psychiatry" },
                    new PlrExpertise { Code = "SMD41",    Name = "Geriatrics" },
                    new PlrExpertise { Code = "SMD42",    Name = "Geriatrics & Psychotherapy" },
                    new PlrExpertise { Code = "SMD43",    Name = "Glaucoma" },
                    new PlrExpertise { Code = "SMD44",    Name = "Hair Restoration Surgery" },
                    new PlrExpertise { Code = "SMD45",    Name = "Hand &Wrist Surgery" },
                    new PlrExpertise { Code = "SMD46",    Name = "Head and Neck Oncology" },
                    new PlrExpertise { Code = "SMD47",    Name = "Hypnotherapy" },
                    new PlrExpertise { Code = "SMD48",    Name = "Joint Replacement & Reconstruction" },
                    new PlrExpertise { Code = "LPNORTHO", Name = "Orthopaedic" },
                    new PlrExpertise { Code = "402",      Name = "Adolescent Medicine" },
                    new PlrExpertise { Code = "410",      Name = "Clinical Pharmacology" },
                    new PlrExpertise { Code = "412",      Name = "Colorectal Surgery" },
                    new PlrExpertise { Code = "414",      Name = "Developmental Pediatrics" },
                    new PlrExpertise { Code = "423",      Name = "General Surgical Oncology" },
                    new PlrExpertise { Code = "427",      Name = "Gynecologic Oncology" },
                    new PlrExpertise { Code = "428",      Name = "Gynecologic Reproductive Endocrinology and Infertility" },
                    new PlrExpertise { Code = "434",      Name = "Neuroradiology" },
                    new PlrExpertise { Code = "445",      Name = "Maternal - Fetal Medicine" },
                    new PlrExpertise { Code = "455",      Name = "Neonatal - Perinatal Medicine" },
                    new PlrExpertise { Code = "461",      Name = "Occupational Medicine" },
                    new PlrExpertise { Code = "462",      Name = "Pediatric Emergency Medicine" },
                    new PlrExpertise { Code = "468",      Name = "Pediatric Radiology" },
                    new PlrExpertise { Code = "485",      Name = "Transfusion Medicine" },
                    new PlrExpertise { Code = "406",      Name = "Child and Adolescent Psychiatry" },
                    new PlrExpertise { Code = "418",      Name = "Forensic Psychiatry" },
                    new PlrExpertise { Code = "426",      Name = "Geriatric Psychiatry" },
                    new PlrExpertise { Code = "470",      Name = "Pain Medicine" },
                    new PlrExpertise { Code = "421",      Name = "General Internal Medicine" }
                };
            }
        }
    }
}
