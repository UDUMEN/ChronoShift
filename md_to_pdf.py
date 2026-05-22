import re, pathlib
from fpdf import FPDF

md_path = r'C:\Users\udume\projectttt\ChronoShift_Game_Design_Document.md'
pdf_path = r'C:\Users\udume\projectttt\ChronoShift_Game_Design_Document.pdf'

lines = pathlib.Path(md_path).read_text(encoding='utf-8').splitlines()

W = 170  # usable width

def strip_md(text):
    text = re.sub(r'\*\*(.+?)\*\*', r'\1', text)
    text = re.sub(r'\*(.+?)\*',     r'\1', text)
    text = re.sub(r'`(.+?)`',       r'\1', text)
    text = re.sub(r'\[(.+?)\]\(.+?\)', r'\1', text)
    return text

class PDF(FPDF):
    def header(self):
        self.set_fill_color(50, 0, 120)
        self.rect(0, 0, 210, 14, 'F')
        self.set_font('Arial', 'B', 9)
        self.set_text_color(255, 255, 255)
        self.set_xy(0, 0)
        self.cell(210, 14, 'ChronoShift  -  Game Design Document', align='C')
        self.set_text_color(0, 0, 0)
        self.set_xy(20, 20)

    def footer(self):
        self.set_y(-12)
        self.set_font('Arial', '', 8)
        self.set_text_color(120, 120, 120)
        self.cell(0, 10, f'Sayfa {self.page_no()}', align='C')

pdf = PDF()
pdf.set_auto_page_break(auto=True, margin=18)
pdf.add_font('Arial',  '',   r'C:\Windows\Fonts\arial.ttf')
pdf.add_font('Arial',  'B',  r'C:\Windows\Fonts\arialbd.ttf')
pdf.add_font('Arial',  'I',  r'C:\Windows\Fonts\ariali.ttf')
pdf.add_font('Arial',  'BI', r'C:\Windows\Fonts\arialbi.ttf')
pdf.add_page()
pdf.set_left_margin(20)
pdf.set_right_margin(20)

table_rows = []

def flush_table():
    if not table_rows:
        return
    max_cols = max(len(r) for r in table_rows)
    col_w = W / max_cols
    for ri, row in enumerate(table_rows):
        if ri == 0:
            pdf.set_fill_color(98, 0, 179)
            pdf.set_text_color(255, 255, 255)
            pdf.set_font('Arial', 'B', 9)
        else:
            pdf.set_fill_color(245, 235, 255) if ri % 2 == 0 else pdf.set_fill_color(255, 255, 255)
            pdf.set_text_color(0, 0, 0)
            pdf.set_font('Arial', '', 9)
        pdf.set_x(20)
        for ci in range(max_cols):
            cell = strip_md(row[ci]) if ci < len(row) else ''
            pdf.cell(col_w, 7, cell[:50], border=1, fill=True)
        pdf.ln()
    pdf.ln(3)
    pdf.set_text_color(0, 0, 0)
    table_rows.clear()

def normal(txt, size=10, style=''):
    pdf.set_left_margin(20)
    pdf.set_x(20)
    pdf.set_font('Arial', style, size)
    pdf.multi_cell(W, 6, txt)

for line in lines:
    raw = line.rstrip()

    if re.match(r'^---+$', raw.strip()):
        flush_table()
        pdf.set_draw_color(140, 48, 217)
        pdf.set_line_width(0.4)
        y = pdf.get_y()
        pdf.line(20, y, 190, y)
        pdf.ln(3)
        continue

    if '|' in raw and raw.strip().startswith('|'):
        if re.match(r'^\|[-| :]+\|$', raw.strip()):
            continue
        table_rows.append([c.strip() for c in raw.strip('|').split('|')])
        continue
    else:
        flush_table()

    if re.match(r'^# [^#]', raw):
        txt = strip_md(raw[2:])
        pdf.set_left_margin(20); pdf.set_x(20)
        pdf.set_font('Arial', 'B', 22)
        pdf.set_text_color(58, 0, 111)
        pdf.ln(4)
        pdf.multi_cell(W, 11, txt)
        pdf.set_draw_color(139, 48, 217); pdf.set_line_width(1)
        pdf.line(20, pdf.get_y(), 190, pdf.get_y())
        pdf.ln(4)
        pdf.set_text_color(0, 0, 0)

    elif re.match(r'^## [^#]', raw):
        txt = strip_md(raw[3:])
        pdf.set_left_margin(20); pdf.set_x(20)
        pdf.set_font('Arial', 'B', 15)
        pdf.set_text_color(92, 0, 153)
        pdf.ln(5)
        pdf.multi_cell(W, 8, txt)
        pdf.set_draw_color(200, 200, 200); pdf.set_line_width(0.3)
        pdf.line(20, pdf.get_y(), 190, pdf.get_y())
        pdf.ln(2)
        pdf.set_text_color(0, 0, 0)

    elif re.match(r'^### [^#]', raw):
        txt = strip_md(raw[4:])
        pdf.set_left_margin(20); pdf.set_x(20)
        pdf.set_font('Arial', 'B', 12)
        pdf.set_text_color(122, 0, 204)
        pdf.ln(3)
        pdf.multi_cell(W, 7, txt)
        pdf.set_text_color(0, 0, 0)

    elif raw.startswith('> '):
        txt = strip_md(raw[2:])
        pdf.set_fill_color(245, 235, 255)
        pdf.set_left_margin(28); pdf.set_x(28)
        pdf.set_font('Arial', 'I', 10)
        pdf.multi_cell(W - 8, 6, txt, fill=True)
        pdf.set_left_margin(20)

    elif re.match(r'^[-*] ', raw):
        txt = strip_md(raw[2:])
        pdf.set_left_margin(20); pdf.set_x(20)
        pdf.set_font('Arial', '', 10)
        pdf.set_text_color(0, 0, 0)
        pdf.cell(8, 6, chr(149))
        pdf.multi_cell(W - 8, 6, txt)

    elif re.match(r'^\d+\.', raw):
        txt = strip_md(re.sub(r'^\d+\.\s*', '', raw))
        num = re.match(r'^(\d+)', raw).group(1)
        pdf.set_left_margin(20); pdf.set_x(20)
        pdf.set_font('Arial', '', 10)
        pdf.set_text_color(0, 0, 0)
        pdf.cell(8, 6, num + '.')
        pdf.multi_cell(W - 8, 6, txt)

    elif raw.strip() == '':
        pdf.ln(2)

    else:
        txt = strip_md(raw)
        if txt:
            pdf.set_text_color(0, 0, 0)
            normal(txt)

flush_table()
pdf.output(pdf_path)
print('OK:', pdf_path)
